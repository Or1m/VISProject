using BusinessLayer.BusinessObjects;
using DataLayer.TableDataGateways;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Controllers
{
    public class ReviewsManager
    {
        private static ReviewsManager instance = null;

        private static readonly object lockObj = new object();

        public static ReviewsManager Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new ReviewsManager());
                }
            }
        }

        public bool CreateAndInsertUserReview(string title, int score, int userId, int gameId, DateTime dateTime, int order)
        {
            bool result = UserReviewGateway.Instance.Insert(new UserReview(title, score, userId, gameId, dateTime, order).ToDTO()) > 0;
            if (result)
            {
                DailyStatistics.Instance.NumberOfUserReviews++;
                return true;
            }

            return false;
        }

        public bool CreateAndInsertReviewerReview(string title, int score, string text, int userId, int gameId, DateTime dateTime, int order)
        {
            bool result = ReviewerReviewGateway.Instance.Insert(new ReviewerReview(title, score, text, dateTime, order).ToDTO()) > 0;
            if (result)
            {
                DailyStatistics.Instance.NumberOfReviewerReviews++;
                return true;
            }

            return false;
        }
    }
}