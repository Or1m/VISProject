using BusinessLayer.BusinessObjects.BaseObjects;
using DTO;
using System;
using System.Collections.Generic;

namespace BusinessLayer.BusinessObjects
{
    public class User : Actor
    {
        public string Nick { get; set; }
        
        public List<Game> FavoriteGames { get; set; }
        public List<Reviewer> FavoriteReviewers { get; set; }


        #region Constructors
        public User() : base() { }
        public User(int id, string nick, string firstName, string lastName, char gender, string country, DateTime dateOfBirth, DateTime registrationDate, Category favoriteCategory = null)
            : base(id, firstName, lastName, gender, country, dateOfBirth, registrationDate, favoriteCategory)
        {
            Nick = nick;
        }
        public User(UserDTO DTO, Category favoriteCategory = null)
            : this(DTO.Id, DTO.Nick, DTO.FirstName, DTO.LastName, DTO.Gender, DTO.Country, DTO.DateOfBirth, DTO.RegistrationDate, favoriteCategory) { }
        #endregion

        public override string ToString()
        {
            return Nick + " " + Gender + " " + Country + " " + DateOfBirth.Date + " " + RegistrationDate.Date + " " + FirstName + " " + LastName + " ";
        } 
        public string ToStringHeader()
        {
            return Nick + ", " + Country;
        }
    }
}