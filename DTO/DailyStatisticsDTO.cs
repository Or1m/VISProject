using System;

namespace DTO
{
    public class DailyStatisticsDTO
    {
        public DateTime Date { get; set; }
        public int NumberOfUserReviews { get; set; }
        public int NumberOfReviewerReviews { get; set; }
    }
}