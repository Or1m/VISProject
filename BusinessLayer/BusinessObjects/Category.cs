namespace BusinessLayer.BusinessObjects
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return CategoryId + " " + Name;
        }

        public string ToStringHeader()
        {
            return Name;
        }
    }
}
