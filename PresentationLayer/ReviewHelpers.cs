using BusinessLayer.Controllers;
using PresentationLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class ReviewHelpers
    {
        private static ReviewHelpers instance = null;

        private static readonly object lockObj = new object();

        public static ReviewHelpers Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new ReviewHelpers());
                }
            }
        }

        public EnAddReview CheckAndCreateReview(string title, int score, int userId, int gameId, DateTime dateTime, int order)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
                return EnAddReview.invalidInput;

            if (ReviewsManager.Instance.CreateAndInsert(title, score, userId, gameId, dateTime, order))
                return EnAddReview.successfullyAdded;
            else
                return EnAddReview.somethingWrong;
        }
    }
}
