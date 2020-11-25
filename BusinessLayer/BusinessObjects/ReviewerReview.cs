using BusinessLayer.BusinessObjects.BaseObjects;

namespace BusinessLayer.BusinessObjects
{
    public class ReviewerReview : Review
    {
        public string TextOfReview { get; set; }


        public override string ToString()
        {
            return Title + " " + TextOfReview + " " + Score + " " + Date.Date; 
        }
        public string ToStringHeader()
        {
            return Title + " " + Score + " " + Date.Date;
        }
    }
}
