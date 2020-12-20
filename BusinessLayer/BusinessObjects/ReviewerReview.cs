using BusinessLayer.BusinessObjects.BaseObjects;
using DTO;
using System;

namespace BusinessLayer.BusinessObjects
{
    public class ReviewerReview : Review
    {
        public string TextOfReview { get; set; }

        public ReviewerReview(string title, int score, string text, DateTime date, int orderOfReview, Game game = null, Actor actor = null)
            : base(title, score, date, orderOfReview, game, actor)
        {
            TextOfReview = text;
        }

        public override string ToString()
        {
            return Title + " " + TextOfReview + " " + Score + " " + Date.Date; 
        }
        public string ToStringHeader()
        {
            return Title + " " + Score + " " + Date.Date;
        }

        public ReviewerReviewDTO ToDTO()
        {
            throw new Exception("Non overriden");
        }
    }
}
