using BusinessLayer.BusinessObjects;
using BusinessLayer.BusinessObjects.BaseObjects;
using BusinessLayer.Controllers;
using PresentationLayer.Enums;

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
            try
            {
                return (!(string.IsNullOrEmpty(nick) || string.IsNullOrWhiteSpace(nick))) ? ActorsMangager.Instance.LoadActor(nick, isReviewer) : null;
            }
            catch
            {
                return null;
            }
        }

        public EnFavorite CheckAndAddToFavorite(int actor, int game)
        {
            try
            {
                return ActorsMangager.Instance.AddGameToFavorite(actor, game) ? EnFavorite.sucessfullyAdded : EnFavorite.somethingWentWrong;
            }
            catch
            {
                return EnFavorite.alreadyInFavoriteGames;
            }
        }
    }
}
