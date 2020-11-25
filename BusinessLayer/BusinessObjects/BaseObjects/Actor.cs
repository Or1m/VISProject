using BusinessLayer.BusinessObjects.Behaviour;
using DTO.BaseDTOObjects;
using System;

namespace BusinessLayer.BusinessObjects.BaseObjects
{
    public class Actor : Persistable<ActorDTO>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }

        public Category FavoriteCategory { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }


        #region Constructors
        public Actor() { }
        public Actor(string firstName, string lastName, char gender, string country, DateTime dateOfBirth, DateTime registrationDate, Category favoriteCategory = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Country = country;
            DateOfBirth = dateOfBirth;

            RegistrationDate = registrationDate;
            FavoriteCategory = favoriteCategory;
        }
        public Actor(ActorDTO DTO, Category favoriteCategory = null)
            : this(DTO.FirstName, DTO.LastName, DTO.Gender, DTO.Country, DTO.DateOfBirth, DTO.RegistrationDate, favoriteCategory) { }
        #endregion

        #region DTO
        public int Id { get; set; }
        public bool IsPersisted { get; set; }

        public ActorDTO ToDTO()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}