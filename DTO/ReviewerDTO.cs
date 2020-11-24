using System;

namespace DTO
{
    public class ReviewerDTO
    {
        public int ReviewerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }
        public string Work { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int? FavoritCategoryId { get; set; }
        public CategoryDTO FavoritCategory { get; set; }
        public int? Deleted { get; set; }
    }
}
