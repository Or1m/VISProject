using System;
using System.Collections.Generic;

namespace DTO
{
    public class UserDTO
    {
        public int User_id { get; set; }
        public string Nick { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }
        public DateTime Date_of_birth { get; set; }
        public DateTime Registration_date { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public int? Favorit_category_id { get; set; }
        public int? Deleted { get; set; }
        public CategoryDTO Favorit_category { get; set; }

        public List<GameDTO> Favorit_games { get; set; }
        public List<ReviewerDTO> Favorit_reviewers { get; set; }
    }
}
