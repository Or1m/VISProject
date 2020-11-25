using DTO.BaseDTOObjects;
using System.Collections.Generic;

namespace DTO
{
    public class UserDTO : ActorDTO
    {
        public string Nick { get; set; }
        
        public List<GameDTO> FavoriteGames { get; set; }
        public List<ReviewerDTO> FavoriteReviewers { get; set; }
    }
}
