namespace BusinessLayer.BusinessObjects.Behaviour
{
    interface Persistable<T>
    {
        bool IsPersisted { get; set; }

        T ToDTO();
    }
}
