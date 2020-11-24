using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessObjects
{
    public class User_review
    {
        public string Title { get; set; }
        public int Score { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public DateTime Date { get; set; }
        public int OrderOfReview { get; set; }

        public User User { get; set; }
        public Game Game { get; set; }


        public User_review() { }
        public User_review(string title, int score, int userId, int gameId, DateTime date, int order_of_review, User user = null, Game game = null)
        {
            Title = title;
            Score = score;
            UserId = userId;
            GameId = gameId;
            Date = date;
            OrderOfReview = order_of_review;
            User = user;
            Game = game;
        }

        public override string ToString()
        {
            return Title + " " + Score + " (" + Date.Date.ToString("dd/MM/yyyy") + ")";
        }
    }
}
