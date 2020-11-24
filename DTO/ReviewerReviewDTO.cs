using System;

namespace DTO
{
    public class ReviewerReviewDTO
    {
        public string Title { get; set; }
        public string TextOfReview { get; set; }
        public int Score { get; set; }
        public int ReviewerId { get; set; }
        public int GameId { get; set; }
        public DateTime Date { get; set; }
        public int OrderOfReview { get; set; }

        public ReviewerDTO Reviewer { get; set; }
        public GameDTO Game { get; set; }
    }
}
