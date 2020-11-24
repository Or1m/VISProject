using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MainClasses
{
    public class Game
    {
        public int Game_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Rating { get; set; }
        public DateTime? Release_date { get; set; }
        public float? Average_user_review { get; set; }
        public float? Average_reviewer_score { get; set; }

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
            Release_date = release_date;
            Average_user_review = average_user_review;
            Average_reviewer_score = average_reviewer_score;

            Categories = new List<Category>();
        }

        public override string ToString()
        {
            return Name + " " + Description + " " + Developer + " " + Rating + " " + Release_date + " " + Average_reviewer_score + " " + Average_user_review;
        }

        public string ToStringHeader()
        {
            return Name + ", " +  Developer;
        }
    }
}
