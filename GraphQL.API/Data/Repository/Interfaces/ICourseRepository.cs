using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.API.Data.Entities;

namespace GraphQL.API.Data.Repository.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllCourses(CancellationToken cancellationToken);
        Task<Course> GetCourse(int courseId, CancellationToken cancellationToken);
        Task<Course> AddCourse(Course course, CancellationToken cancellationToken);
        Task<bool> CourseExists(string courseName, CancellationToken cancellationToken);
        Task<Course> UpdateCourse(int courseId, Course course, CancellationToken cancellationToken);
        Task<Course> DeleteCourse(int courseId, CancellationToken cancellationToken);
    }
}
