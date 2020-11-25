using BusinessLayer.BusinessObjects.Behaviour;
using DTO;

namespace BusinessLayer.BusinessObjects
{
    public class Category : Persistable<CategoryDTO>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public CategoryDTO ToDTO()
        {
            throw new System.NotImplementedException();
        }

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
