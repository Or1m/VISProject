using System;
using System.Collections.Generic;

namespace DTO
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Rating { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public float? AverageUserScore { get; set; }
        public float? AverageReviewerScore { get; set; }

        public List<CategoryDTO> Categories { get; set; }
    }
}
