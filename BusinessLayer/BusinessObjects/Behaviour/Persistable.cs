namespace BusinessLayer.BusinessObjects.Behaviour
{
    interface Persistable<T>
    {
        int Id { get; set; }
        bool IsPersisted { get; set; }

        T ToDTO();
    }
}
