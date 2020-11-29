namespace BusinessLayer.BusinessObjects.Behaviour
{
    public abstract class Persistable<T>
    {
        public int Id { get; set; }
        public bool IsPersisted { get; set; }

        public abstract T ToDTO();
    }
}
