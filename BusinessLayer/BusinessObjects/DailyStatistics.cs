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

        public DailyStatistics()
        {
            Date = DateTime.Now;
        }


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
