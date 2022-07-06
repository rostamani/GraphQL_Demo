using GraphQL.Types;

namespace GraphQL.API.GraphQL.Types
{
    public class CourseInputType : InputObjectGraphType
    {
        public CourseInputType()
        {
            Name = "CourseInputType";
            Description = "This type is used to add a new course";

            Field<NonNullGraphType<StringGraphType>>("Name", "Name of the course");
            Field<NonNullGraphType<StringGraphType>>("Description", "Description of the course");
            Field<ListGraphType<ReviewInputType>>("Reviews", "Reviews of the course");
        }
    }
}
