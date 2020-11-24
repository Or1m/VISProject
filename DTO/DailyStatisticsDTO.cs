using System;

namespace DTO
{
    public class DailyStatisticsDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int number_of_reviews { get; set; }
        public string Type { get; set; }
    }
}
