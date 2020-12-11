
namespace BusinessLayer.BusinessObjects
{
    public class Email<T, U, Q>
    {
        public T t { get; set; }
        public U u { get; set; }
        public Q q { get; set; }

        public Email(T t, U u) : this(t, u, default) { }

        public Email(T t, U u, Q q)
        {
            this.t = t;
            this.u = u;
            this.q = q;
        }
    }
}
