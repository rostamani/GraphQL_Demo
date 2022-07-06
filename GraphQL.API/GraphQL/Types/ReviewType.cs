using GraphQL.API.Data.Entities;
using GraphQL.Types;

namespace GraphQL.API.GraphQL.Types
{
    public class ReviewType : ObjectGraphType<Review>
    {
        public ReviewType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Comment, type: typeof(StringGraphType));
            Field(x => x.Rate, type: typeof(IntGraphType));
        }
    }
}
