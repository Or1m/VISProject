using BusinessLayer.BusinessObjects.BaseObjects;

namespace BusinessLayer.BusinessObjects
{
    public class UserReview : Review
    {
        public override string ToString()
        {
            return Title + " " + Score + " (" + Date.Date.ToString("dd/MM/yyyy") + ")";
        }
    }
}
