using BusinessLayer.BusinessObjects.BaseObjects;
using DTO;
using DTO.BaseDTOObjects;
using System;

namespace BusinessLayer.BusinessObjects
{
    public class UserReview : Review
    {
        #region Constructors
        public UserReview() { }
        public UserReview(string title, int score, int userId, int gameId, DateTime dateTime, int order)
        : base(title, score, dateTime, order)
        {
            Game = new Game();
            Game.Id = gameId;

            Actor = new User();
            Actor.Id = userId;
        }
        #endregion

        public override string ToString()
        {
            return Title + " " + Score + " (" + Date.Date.ToString("dd/MM/yyyy") + ")";
        }

        public UserReviewDTO ToDTO()
        {
            UserReviewDTO dto = new UserReviewDTO();
            dto.Title = Title;
            dto.Score = Score;
            dto.Date = Date;
            dto.OrderOfReview = OrderOfReview;

            dto.Game = new GameDTO();
            dto.Game.Id = Game.Id;

            dto.Actor = new ActorDTO();
            dto.Actor.Id = Actor.Id;

            return dto;
        }
    }
}
