using BusinessLayer.BusinessObjects.Behaviour;
using DTO;
using DTO.BaseDTOObjects;
using System;

namespace BusinessLayer.BusinessObjects.BaseObjects
{
    public class Review : Persistable<ReviewDTO>
    {
        #region Public Properties
        public string Title { get; set; }
        public int Score { get; set; }

        public Game Game { get; set; }
        public Actor Actor { get; set; }
        public Category FavoriteCategory { get; set; }

        public DateTime Date { get; set; }
        public int OrderOfReview { get; set; }
        #endregion


        #region Constructors
        public Review(Game game = null, Actor actor = null)
        {
            Game = game;
            Actor = actor;
        }
        public Review(string title, int score, DateTime date, int orderOfReview, Game game = null, Actor actor = null) : this(game, actor)
        {
            Title = title;
            Score = score;
            Date = date;
            OrderOfReview = orderOfReview;
        }
        public Review(ReviewDTO DTO, Game game = null, Actor actor = null) 
            : this(DTO.Title, DTO.Score, DTO.Date, DTO.OrderOfReview, game, actor) { }
        #endregion

        #region DTO
        public override ReviewDTO ToDTO()
        {
            throw new Exception("Non overriden");
        }
        #endregion

        public override string ToString()
        {
            return Title + " " + Score;
        }
    }
}
