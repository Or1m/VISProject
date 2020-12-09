namespace DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public CategoryDTO(string name)
        {
            Name = name;
        }
    }
}
