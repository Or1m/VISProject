using System;

namespace DTO.BaseDTOObjects
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }
        
        public CategoryDTO FavoriteCategory { get; set; }
    }
}
