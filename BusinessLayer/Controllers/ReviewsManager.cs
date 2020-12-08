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

        public bool CreateAndInsert(string title, int score, int userId, int gameId, DateTime dateTime, int order)
        {
            return UserReviewGateway.Instance.Insert(new UserReview(title, score, userId, gameId, dateTime, order).ToDTO()) > 0;
        }
    }
}
