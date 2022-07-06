using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using GraphQL.API.Data.Context;
using GraphQL.API.Data.Repository;
using GraphQL.API.Data.Repository.Interfaces;
using GraphQL.API.GraphQL;
using GraphQL.API.GraphQL.Mutations;
using GraphQL.API.GraphQL.Queries;
using GraphQL.Server;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("GraphQLConnectionString"));
            });


            services.AddScoped<CourseQuery>();
            services.AddScoped<CourseMutation>();
            services.AddScoped<AppSchema>();

            services.AddGraphQL(options =>
            {
                options.UnhandledExceptionDelegate = ctx =>
                {
                    Console.WriteLine(ctx.Exception.StackTrace);
                };
            }).AddSystemTextJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context)
        {
            context.SeedData();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseGraphQL<AppSchema>();
            app.UseGraphQLGraphiQL("/ui/graphql");

            app.UseRouting();


        }
    }
}
