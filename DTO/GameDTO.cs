using System;
using System.Collections.Generic;

namespace DTO
{
    public class GameDTO
    {
        public int Game_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Rating { get; set; }
        public DateTime? Release_date { get; set; }
        public float? Average_user_review { get; set; }
        public float? Average_reviewer_score { get; set; }

        public List<CategoryDTO> Categories { get; set; }
    }
}
