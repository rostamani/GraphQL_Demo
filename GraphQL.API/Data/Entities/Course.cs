using System;
using System.Collections.Generic;

namespace GraphQL.API.Data.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual List<Review> Reviews { get; set; }
    }
}
