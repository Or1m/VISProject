using BusinessLayer.BusinessObjects.Behaviour;
using DTO;
using System;

namespace BusinessLayer.BusinessObjects
{
    public class DailyStatistics : Persistable<DailyStatisticsDTO>
    {
        #region Public Properties
        public DateTime Date { get; set; }
        public int NumberOfUserReviews { get; set; }
        public int NumberOfReviewerReviews { get; set; }
        #endregion


        #region Constructor & Singleton Pattern
        private static DailyStatistics instance = null;

        private static readonly object lockObj = new object();

        public static DailyStatistics Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new DailyStatistics());
                }
            }
        }
        private DailyStatistics()
        {
            Date = DateTime.Now;
        }
        #endregion


        public override DailyStatisticsDTO ToDTO()
        {
            DailyStatisticsDTO dto = new DailyStatisticsDTO()
            {
                Date = Date,
                NumberOfUserReviews = NumberOfUserReviews,
                NumberOfReviewerReviews = NumberOfReviewerReviews
            };

            return dto;
        }

        public override string ToString()
        {
            return Date.Date + " " + NumberOfUserReviews + " " + NumberOfReviewerReviews;
        }
    }
}
