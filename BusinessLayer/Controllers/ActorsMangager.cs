using BusinessLayer.BusinessObjects;
using BusinessLayer.BusinessObjects.BaseObjects;
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

        public Actor LoadActor(string nick, bool isReviewer) 
        {
            if(!isReviewer)
                return new User(UserGateway.Instance.SelectUserByNickWithCategory(nick));
            else
                return new Reviewer(ReviewerGateway.Instance.SelectReviewerByIdWithCategory(0)); // TODO fixne idečko
        }
    }
}
