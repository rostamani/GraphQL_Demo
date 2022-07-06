using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.API.Data.Context;
using GraphQL.API.Data.Entities;
using GraphQL.API.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.API.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Course>> GetAllCourses(CancellationToken cancellationToken)
        {
            return await _context.Courses
                .Include(c=>c.Reviews)
                .ToListAsync(cancellationToken);
        }

        public async Task<Course> GetCourse(int courseId, CancellationToken cancellationToken)
        {
            return await _context.Courses.Include(x=>x.Reviews)
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id == courseId, cancellationToken: cancellationToken);
        }

        public async Task<Course> AddCourse(Course course, CancellationToken cancellationToken)
        {
            await _context.Courses.AddAsync(course, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return course;
        }

        public async Task<bool> CourseExists(string courseName, CancellationToken cancellationToken)
        {
            return await _context.Courses.AsNoTracking()
                .AnyAsync(x => x.Name.ToLower() == courseName.ToLower(), cancellationToken);
        }

        public async Task<Course> UpdateCourse(int courseId, Course courseInputDto, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x=> x.Id == courseId, cancellationToken);
            course.UpdateDate = DateTime.Now;
            course.Name = courseInputDto.Name;
            course.Description = courseInputDto.Description;

            await _context.SaveChangesAsync(cancellationToken);
            return course;

        }

        public async Task<Course> DeleteCourse(int courseId, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId, cancellationToken);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(cancellationToken);
            return course;
        }
    }
}
