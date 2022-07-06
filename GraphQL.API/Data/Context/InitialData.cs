using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.API.Data.Entities;

namespace GraphQL.API.Data.Context
{
    public static class InitialData
    {
        public static void SeedData(this AppDbContext context)
        {
            if (!context.Courses.Any())
            {
                var courses = new List<Course>();

                courses.Add(new Course()
                {
                    Name = "Math",
                    Description = "Math class",
                    AddDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Reviews = new List<Review>()
                    {
                        new Review()
                        {
                            Comment = "it was a very good course",
                            Rate = 5
                        }
                    }
                });

                courses.Add(new Course()
                {
                    Name = "Physics",
                    Description = "Physics class",
                    AddDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Reviews = new List<Review>()
                    {
                        new Review()
                        {
                            Comment = "it was terrible!",
                            Rate = 0
                        }
                    }
                });

                courses.Add(new Course()
                {
                    Name = "literature",
                    Description = "literature class",
                    AddDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                context.Courses.AddRange(courses);
                context.SaveChanges();
            }

        }
    }
}
