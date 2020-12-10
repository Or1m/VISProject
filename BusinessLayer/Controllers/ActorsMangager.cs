using BusinessLayer.BusinessObjects;
using BusinessLayer.BusinessObjects.BaseObjects;
using DataLayer.TableDataGateways;
using DTO;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Controllers
{
    public class ActorsManager
    {
        private static ActorsManager instance = null;

        private static readonly object lockObj = new object();

        public static ActorsManager Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new ActorsManager());
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

        public List<Review> LoadReviewsForUser(int userId)
        {
            return UserReviewGateway.Instance.SelectReviewsForUser(userId).ConvertAll(r => new Review(r));
        }
    }
}
