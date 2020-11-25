using System;

namespace DTO.BaseDTOObjects
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Score { get; set; }

        public GameDTO Game { get; set; }
        public ActorDTO Actor { get; set; }
        public CategoryDTO FavoriteCategory { get; set; }

        public DateTime Date { get; set; }
        public int OrderOfReview { get; set; }
    }
}
