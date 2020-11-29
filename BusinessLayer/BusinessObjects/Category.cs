using BusinessLayer.BusinessObjects.Behaviour;
using DTO;

namespace BusinessLayer.BusinessObjects
{
    public class Category : Persistable<CategoryDTO>
    {
        public string Name { get; set; }


        #region Constructors
        public Category() { }
        public Category(string name)
        {
            Name = name;
        }
        public Category(CategoryDTO DTO) : this(DTO.Name) { }
        #endregion

        public override string ToString()
        {
            return Id + " " + Name;
        }
        public string ToStringHeader()
        {
            return Name;
        }

        #region DTO
        public override CategoryDTO ToDTO()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
