using System;

namespace DTO
{
    public class DailyStatisticsDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfReviews { get; set; }
        public string Type { get; set; }
    }
}
