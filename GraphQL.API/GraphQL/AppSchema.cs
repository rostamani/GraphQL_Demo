using GraphQL.API.GraphQL.Mutations;
using GraphQL.API.GraphQL.Queries;
using GraphQL.Types;

namespace GraphQL.API.GraphQL
{
    public class AppSchema : Schema
    {
        public AppSchema(CourseQuery courseQuery, CourseMutation courseMutation)
        {
            Query = courseQuery;
            Mutation = courseMutation;
        }
    }
}
