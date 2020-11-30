using BusinessLayer.BusinessObjects.BaseObjects;
using DTO;
using System;

namespace BusinessLayer.BusinessObjects
{
    public class Reviewer : Actor
    {
        public string Work { get; set; }


        #region Constructors
        public Reviewer() : base() { }
        public Reviewer(int id, string firstName, string lastName, char gender, string country, string work, DateTime dateOfBirth, DateTime registrationDate, Category favoriteCategory = null)
            : base(id, firstName, lastName, gender, country, dateOfBirth, registrationDate, favoriteCategory) 
        {
            Work = work;
        }
        public Reviewer(ReviewerDTO DTO, Category favoriteCategory = null)
            : this(DTO.Id, DTO.FirstName, DTO.LastName, DTO.Gender, DTO.Country, DTO.Work, DTO.DateOfBirth, DTO.RegistrationDate, favoriteCategory) { }
        #endregion

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Gender + " " + Work + " " + Country + " " + DateOfBirth.Date + " " + RegistrationDate.Date + " ";
        }
        public string ToStringHeader()
        {
            return FirstName + " " + LastName + " [ " + Work + " ]" + ", " + Country;
        }
    }
}
