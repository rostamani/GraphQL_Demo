using GraphQL.Types;

namespace GraphQL.API.GraphQL.Types
{
    public class ReviewInputType : InputObjectGraphType
    {
        public ReviewInputType()
        {
            Name = "Review";
            Field<NonNullGraphType<StringGraphType>>("comment");
            Field<NonNullGraphType<IntGraphType>>("rate");
        }
    }
}
