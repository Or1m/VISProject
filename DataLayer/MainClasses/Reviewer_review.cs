using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MainClasses
{
    public class Reviewer_review
    {
        public string Title { get; set; }
        public string Text_of_review { get; set; }
        public int Score { get; set; }
        public int ReviewerId { get; set; }
        public int GameId { get; set; }
        public DateTime Date { get; set; }
        public int Order_of_review { get; set; }

        public Reviewer Reviewer { get; set; }
        public Game Game { get; set; }

        public Reviewer_review() { }
        public Reviewer_review(string title, string text_of_review, int score, int reviewerId, int gameId, DateTime date, int order_of_review, Reviewer reviewer = null, Game game = null)
        {
            Title = title;
            Text_of_review = text_of_review;
            Score = score;
            ReviewerId = reviewerId;
            GameId = gameId;
            Date = date;
            Order_of_review = order_of_review;
            Reviewer = reviewer;
            Game = game;
        }

        public override string ToString()
        {
            return Title + " " + Text_of_review + " " + Score + " " + Date.Date; 
        }

        public string ToStringHeader()
        {
            return Title + " " + Score + " " + Date.Date;
        }
    }
}
