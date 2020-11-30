using BusinessLayer.BusinessObjects;
using BusinessLayer.Controllers;

namespace PresentationLayer
{
    public class ActorHelpers
    {
        private static ActorHelpers instance = null;

        private static readonly object lockObj = new object();

        public static ActorHelpers Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new ActorHelpers());
                }
            }
        }

        public User LoadUser(string nick)
        {
            return (!(string.IsNullOrEmpty(nick) || string.IsNullOrWhiteSpace(nick))) ? ActorsMangager.Instance.LoadUser(nick) : null;
        }
    }
}
