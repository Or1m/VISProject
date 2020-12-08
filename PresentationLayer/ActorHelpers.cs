using BusinessLayer.BusinessObjects;
using BusinessLayer.BusinessObjects.BaseObjects;
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

        public Actor LoadActor(string nick, bool isReviewer)
        {
            return (!(string.IsNullOrEmpty(nick) || string.IsNullOrWhiteSpace(nick))) ? ActorsMangager.Instance.LoadActor(nick, isReviewer) : null;
        }
    }
}
