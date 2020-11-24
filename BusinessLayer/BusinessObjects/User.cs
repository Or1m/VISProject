using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessObjects
{
    public class User
    {
        public int User_id { get; set; }
        public string Nick { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }
        public DateTime Date_of_birth { get; set; }
        public DateTime Registration_date { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public int? Favorit_category_id { get; set; }
        public int? Deleted { get; set; }
        public Category Favorit_category { get; set; }

        public List<Game> Favorit_games { get; set; }
        public List<Reviewer> Favorit_reviewers { get; set; }

        // Constructors
        public User() { }

        public User(string nick, char gender, string country, DateTime date_of_birth, DateTime registration_date, string first_name, string last_name, int? favorit_category_id = null, int? deleted = null, Category favorit_category = null)
        {
            Nick = nick;
            Gender = gender;
            Country = country;
            Date_of_birth = date_of_birth;
            Registration_date = registration_date;
            First_name = first_name;
            Last_name = last_name;
            Favorit_category_id = favorit_category_id;
            Deleted = deleted;
            Favorit_category = favorit_category;
        }

        public override string ToString()
        {
            return Nick + " " + Gender + " " + Country + " " + Date_of_birth.Date + " " + Registration_date.Date + " " + First_name + " " + Last_name + " " + Favorit_category_id + " " + Deleted;
        } 

        public string ToStringHeader()
        {
            return Nick + ", " + Country;
        }
    }
}
