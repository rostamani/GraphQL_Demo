using System;
using GraphQL.API.Data.Entities;
using GraphQL.API.Data.Repository.Interfaces;
using GraphQL.API.GraphQL.Types;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace GraphQL.API.GraphQL.Mutations
{
    public class CourseMutation : ObjectGraphType
    {
        public CourseMutation(ICourseRepository courseRepository, IHttpContextAccessor httpContextAccessor)
        {
            FieldAsync<CourseType>("addCourse",
                "It is used to add a new course to the database",
                arguments: new QueryArguments()
                {
                    new QueryArgument<NonNullGraphType<CourseInputType>>
                        { Name = "courseInputDto", Description = "Course input parameter" }
                },
                resolve: async context =>
                {
                    var course = context.GetArgument<Course>("courseInputDto");
                    if (!await courseRepository.CourseExists(course.Name,httpContextAccessor.HttpContext.RequestAborted))
                    {
                        course.UpdateDate = course.AddDate = DateTime.Now;
                        return await courseRepository.AddCourse(course, httpContextAccessor.HttpContext.RequestAborted);
                    }

                    context.Errors.Add(new ExecutionError("There is already a course with the given name."));
                    return null;
                });


            FieldAsync<CourseType>("updateCourse",
                "This is used to update existing course in the database",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CourseInputType>> { Name = "courseInputDto" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "courseId" }),
                resolve: async context =>
                {
                    var cancellationToken = httpContextAccessor.HttpContext.RequestAborted;
                    var courseId = context.GetArgument<int>("courseId");
                    var courseInputDto = context.GetArgument<Course>("courseInputDto");
                    var course =
                        await courseRepository.GetCourse(courseId, cancellationToken);
                    if (course is not null)
                    {
                        if (await courseRepository.CourseExists(courseInputDto.Name, cancellationToken) == false)
                        {
                            return await courseRepository.UpdateCourse(courseId, course, cancellationToken);
                        }

                        context.Errors.Add(new ExecutionError("There is already a course with the given name."));
                        return null;
                    }

                    context.Errors.Add(new ExecutionError("There is not any course with the given courseId."));
                    return null;
                });


            FieldAsync<CourseType>("deleteCourse",
                "It is uded to delete existing course",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                    { Name = "courseId", Description = "Id of the course to delete" }),
                resolve: async context =>
                {
                    var cancellationToken = httpContextAccessor.HttpContext.RequestAborted;
                    var courseId = context.GetArgument<int>("courseId");
                    if (await courseRepository.GetCourse(courseId, cancellationToken) != null)
                    {
                        return await courseRepository.DeleteCourse(courseId, cancellationToken);
                    }

                    context.Errors.Add(new ExecutionError("There is not any course with the given courseId."));
                    return null;
                });
        }
    }
}
