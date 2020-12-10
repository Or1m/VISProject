
namespace BusinessLayer.BusinessObjects
{
    class Email<T, U, Q>
    {
        T t;
        U u;
        Q q;

        public Email(T t, U u) : this(t, u, default) { }

        public Email(T t, U u, Q q)
        {
            this.t = t;
            this.u = u;
            this.q = q;
        }
    }
}
