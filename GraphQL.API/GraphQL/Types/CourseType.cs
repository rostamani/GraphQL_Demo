using GraphQL.API.Data.Entities;
using GraphQL.Types;

namespace GraphQL.API.GraphQL.Types
{
    public class CourseType : ObjectGraphType<Course>
    {
        public CourseType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name, type: typeof(StringGraphType));
            Field(x => x.Description, type: typeof(StringGraphType));
            Field(x => x.AddDate, type: typeof(DateTimeGraphType));
            Field(x => x.UpdateDate, type: typeof(DateTimeGraphType));

            Field(x=>x.Reviews, type: typeof(ListGraphType<ReviewType>));
        }
    }
}
