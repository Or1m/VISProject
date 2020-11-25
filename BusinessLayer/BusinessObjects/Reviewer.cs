using BusinessLayer.BusinessObjects.BaseObjects;
using BusinessLayer.BusinessObjects.Behaviour;
using DTO;
using System;

namespace BusinessLayer.BusinessObjects
{
    public class Reviewer : Actor, Persistable<ReviewerDTO>
    {
        public string Work { get; set; }
        
        

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Gender + " " + Work + " " + Country + " " + DateOfBirth.Date + " " + RegistrationDate.Date + " " + FavoriteCategoryId + " " + Deleted;
        }
        public string ToStringHeader()
        {
            return FirstName + " " + LastName + " [ " + Work + " ]" + ", " + Country;
        }

        public ReviewerDTO ToDTO()
        {
            throw new NotImplementedException();
        }

        public Reviewer() { }
        public Reviewer(string first_name, string last_name, char gender, string country, string work, DateTime date_of_birth, DateTime registration_date, int? favorit_category_id = null, Category favorit_category = null, int ? deleted = null)
        {
            FirstName = first_name;
            LastName = last_name;
            Gender = gender;
            Country = country;
            Work = work;
            DateOfBirth = date_of_birth;
            RegistrationDate = registration_date;
            FavoriteCategoryId = favorit_category_id;
            FavoriteCategory = favorit_category;
            Deleted = deleted;
        }
    }
}
