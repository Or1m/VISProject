using BusinessLayer.BusinessObjects.Behaviour;
using DTO;
using System;

namespace BusinessLayer.BusinessObjects
{
    public class DailyStatistics : Persistable<DailyStatisticsDTO>
    {
        public DateTime Date { get; set; }
        public int NumberOfUserReviews { get; set; }

        public int NumberOfReviewerReviews { get; set; }


        public override DailyStatisticsDTO ToDTO()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Date.Date + " " + NumberOfUserReviews + " " + NumberOfReviewerReviews;
        }
    }
}
