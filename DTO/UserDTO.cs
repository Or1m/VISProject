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
        public DateTime Date_of_birth { get; set; }
        public DateTime Registration_date { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? FavoritCategoryId { get; set; }
        public int? Deleted { get; set; }
        public CategoryDTO FavoriteCategory { get; set; }

        public List<GameDTO> FavoriteGames { get; set; }
        public List<ReviewerDTO> FavoriteReviewers { get; set; }
    }
}
