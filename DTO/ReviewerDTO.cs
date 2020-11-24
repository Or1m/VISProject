using System;

namespace DTO
{
    public class ReviewerDTO
    {
        public int Reviewer_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }
        public string Work { get; set; }
        public DateTime Date_of_birth { get; set; }
        public DateTime Registration_date { get; set; }
        public int? Favorit_category_id { get; set; }
        public CategoryDTO Favorit_category { get; set; }
        public int? Deleted { get; set; }
    }
}
