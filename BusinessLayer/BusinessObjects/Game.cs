using System;
using System.Collections.Generic;

namespace BusinessLayer.BusinessObjects
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Rating { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public float? AverageUserScore { get; set; }
        public float? AverageReviewerScore { get; set; }

        public List<Category> Categories { get; set; }

        public Game()
        {
            Categories = new List<Category>();
        }
        public Game(string name, string description, string developer, string rating, DateTime? release_date = null, float? average_user_review = null, float? average_reviewer_score = null)
        {
            Name = name;
            Description = description;
            Developer = developer;
            Rating = rating;
            ReleaseDate = release_date;
            AverageUserScore = average_user_review;
            AverageReviewerScore = average_reviewer_score;

            Categories = new List<Category>();
        }

        public override string ToString()
        {
            return Name + " " + Description + " " + Developer + " " + Rating + " " + ReleaseDate + " " + AverageReviewerScore + " " + AverageUserScore;
        }

        public string ToStringHeader()
        {
            return Name + ", " +  Developer;
        }
    }
}
