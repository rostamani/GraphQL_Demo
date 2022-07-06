using GraphQL.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.API.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
