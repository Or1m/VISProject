using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessObjects
{
    public class User
    {
        public int UserId { get; set; }
        public string Nick { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? FavoriteCategoryId { get; set; }
        public int? Deleted { get; set; }
        public Category FavoriteCategory { get; set; }

        public List<Game> FavoriteGames { get; set; }
        public List<Reviewer> FavoriteReviewers { get; set; }

        // Constructors
        public User() { }

        public User(string nick, char gender, string country, DateTime date_of_birth, DateTime registration_date, string first_name, string last_name, int? favorit_category_id = null, int? deleted = null, Category favorit_category = null)
        {
            Nick = nick;
            Gender = gender;
            Country = country;
            DateOfBirth = date_of_birth;
            RegistrationDate = registration_date;
            FirstName = first_name;
            LastName = last_name;
            FavoriteCategoryId = favorit_category_id;
            Deleted = deleted;
            FavoriteCategory = favorit_category;
        }

        public override string ToString()
        {
            return Nick + " " + Gender + " " + Country + " " + DateOfBirth.Date + " " + RegistrationDate.Date + " " + FirstName + " " + LastName + " " + FavoriteCategoryId + " " + Deleted;
        } 

        public string ToStringHeader()
        {
            return Nick + ", " + Country;
        }
    }
}
