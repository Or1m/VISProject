using BusinessLayer.BusinessObjects;
using DataLayer.TableDataGateways;

namespace BusinessLayer.Controllers
{
    public class ActorsMangager
    {
        private static ActorsMangager instance = null;

        private static readonly object lockObj = new object();

        public static ActorsMangager Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new ActorsMangager());
                }
            }
        }

        public User LoadUser(string nick) 
        {
            return new User(UserGateway.Instance.SelectUserByNickWithCategory(nick));
        }
    }
}
