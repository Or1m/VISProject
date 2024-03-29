﻿using BusinessLayer.BusinessObjects.BaseObjects;
using BusinessLayer.Controllers;
using PresentationLayer.Enums;

namespace PresentationLayer.Helpers
{
    public class ActorHelpers
    {
        #region Singleton Pattern
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
        #endregion

        #region Public Methods
        public Actor LoadActor(string nick, bool isReviewer)
        {
            try
            {
                return (!(string.IsNullOrEmpty(nick) || string.IsNullOrWhiteSpace(nick))) ? ActorsManager.Instance.LoadActor(nick, isReviewer) : null;
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
                return ActorsManager.Instance.AddGameToFavorite(actor, game) ? EnFavorite.sucessfullyAdded : EnFavorite.somethingWentWrong;
            }
            catch
            {
                return EnFavorite.alreadyInFavoriteGames;
            }
        }
        #endregion
    }
}