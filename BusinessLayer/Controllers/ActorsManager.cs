using BusinessLayer.BusinessObjects;
using BusinessLayer.BusinessObjects.BaseObjects;
using BusinessLayer.Enums;
using DataLayer.TableDataGateways;
using DTO;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Controllers
{
    #region Constructor & Singleton Pattern
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

        private ActorsManager() { }
        #endregion

        #region Public Methods
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

        public EnBusinessRequest CheckAndRegisterReviewer(string firstName, string lastName, string gender, string country,
            string dateOfBirth, string registrationDate, string work)
        {
            DateTime birth = DateTime.Parse(dateOfBirth);
            DateTime reg = DateTime.Parse(registrationDate);

            if (birth > reg || reg.Year < 2014)
                return EnBusinessRequest.dateMismatch;

            Reviewer reviewer = new Reviewer()
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender.ToCharArray()[0],
                Country = country,
                Work = work,
                RegistrationDate = reg,
                DateOfBirth = birth
            };

            return ReviewerGateway.Instance.Insert(reviewer.ToDTO()) > 0 ? EnBusinessRequest.sucess : EnBusinessRequest.somethingWrong;
        }
        #endregion
    }
}