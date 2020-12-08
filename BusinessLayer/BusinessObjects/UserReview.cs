using BusinessLayer.BusinessObjects.BaseObjects;
using DTO.BaseDTOObjects;
using System;

namespace BusinessLayer.BusinessObjects
{
    public class UserReview : Review
    {
        public UserReview() { }
        public UserReview(string title, int score, int userId, int gameId, DateTime dateTime, int order)
        : base(title, score, dateTime, order)
        {
            Game = new Game();
            Game.Id = gameId;

            Actor = new User();
            Actor.Id = userId;
        }

        public override string ToString()
        {
            return Title + " " + Score + " (" + Date.Date.ToString("dd/MM/yyyy") + ")";
        }
    }
}
