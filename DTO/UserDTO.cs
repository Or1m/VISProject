using System;
using System.Collections.Generic;

namespace DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Nick { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? FavoriteCategoryId { get; set; }
        public int? Deleted { get; set; }
        public CategoryDTO FavoriteCategory { get; set; }

        public List<GameDTO> FavoriteGames { get; set; }
        public List<ReviewerDTO> FavoriteReviewers { get; set; }
    }
}
