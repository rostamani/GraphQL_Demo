namespace GraphQL.API.Data.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
