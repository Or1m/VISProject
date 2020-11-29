using BusinessLayer.BusinessObjects.Behaviour;
using DTO;
using System;
using System.Collections.Generic;

namespace BusinessLayer.BusinessObjects
{
    public class Game : Persistable<GameDTO>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Rating { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public float? AverageUserScore { get; set; }
        public float? AverageReviewerScore { get; set; }

        public List<Category> Categories { get; set; }


        #region Constructors
        public Game()
        {
            Categories = new List<Category>();
        }
        public Game(int id, string name, string description, string developer, string rating, DateTime? releaseDate = null, float? averageUserScore = null, float? averageReviewerScore = null) : this()
        {
            Id = id;
            Name = name;
            Description = description;
            Developer = developer;
            Rating = rating;
            ReleaseDate = releaseDate;
            AverageUserScore = averageUserScore;
            AverageReviewerScore = averageReviewerScore;
        }
        public Game(GameDTO DTO) 
            : this(DTO.Id, DTO.Name, DTO.Description, DTO.Developer, DTO.Rating, DTO.ReleaseDate, DTO.AverageUserScore, DTO.AverageReviewerScore) 
        {
            foreach(var catDTO in DTO.Categories)
            {
                Categories.Add(new Category(catDTO));
            }
        }
        #endregion

        public override string ToString()
        {
            return Name + " " + Description + " " + Developer + " " + Rating + " " + ReleaseDate + " " + AverageReviewerScore + " " + AverageUserScore;
        }
        public string ToStringHeader()
        {
            return Name + ", " +  Developer;
        }

        #region DTO
        public int Id { get; set; }
        public bool IsPersisted { get; set; }
        public GameDTO ToDTO()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
