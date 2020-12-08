using BusinessLayer.BusinessObjects;
using BusinessLayer.BusinessObjects.BaseObjects;
using DataLayer.TableDataGateways;
using DTO;
using System;

namespace BusinessLayer.Controllers
{
    public class ActorsMangager
    {
        private static ActorsMangager instance = null;

        private static readonly object lockObj = new object();

        public static ActorsMangager Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new ActorsMangager());
                }
            }
        }

        public Actor LoadActor(string nick, bool isReviewer) 
        {
            if (!isReviewer)
            {
                UserDTO dto = UserGateway.Instance.SelectUserByNickWithCategory(nick);
                return dto != null ? new User(dto) : null;
            }
            else
            {
                string[] name = nick.Split();
                ReviewerDTO dto = ReviewerGateway.Instance.SelectReviewerByNameWithCategory(name[0], name[1]);
                return dto != null ? new Reviewer(dto) : null;
            }
        }

        public bool AddGameToFavorite(int actor, int game)
        {
            return FavoriteGameGateway.Instance.Insert(actor, game) > 0;
        }
    }
}
