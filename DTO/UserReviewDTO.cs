using System;

namespace DTO
{
    public class UserReviewDTO
    {
        public string Title { get; set; }
        public int Score { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public DateTime Date { get; set; }
        public int Order_of_review { get; set; }

        public UserDTO User { get; set; }
        public GameDTO Game { get; set; }
    }
}
