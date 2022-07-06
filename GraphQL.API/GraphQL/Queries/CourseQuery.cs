using GraphQL.API.Data.Repository.Interfaces;
using GraphQL.API.GraphQL.Types;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace GraphQL.API.GraphQL.Queries
{
    public class CourseQuery : ObjectGraphType
    {
        public CourseQuery(ICourseRepository courseRepository , IHttpContextAccessor httpContextAccessor)
        {
            Field<ListGraphType<CourseType>>(
                name:"courses", 
                "Returns the list of all courses",
                resolve: context => courseRepository.GetAllCourses(httpContextAccessor.HttpContext.RequestAborted));

            //Field<CourseType>(
            //    "course",
            //    "Returns the course with specified Id",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
            //        { Name = "courseId", Description = "The id of the desired course" }),
            //    resolve: context => courseRepository.GetCourse(context.GetArgument<int>("courseId", 0), httpContextAccessor.HttpContext.RequestAborted));

            Field<CourseType>()
                .Name("course")
                .Description("Returns the course with specified Id")
                .Argument<NonNullGraphType<IdGraphType>>("courseId", "Id of desired course")
                .Resolve(context => courseRepository.GetCourse(context.GetArgument<int>("courseId", 0), httpContextAccessor.HttpContext.RequestAborted));
        }
    }
}
