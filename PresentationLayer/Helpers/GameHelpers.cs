﻿using BusinessLayer.Controllers;
using PresentationLayer.Enums;
using System;

namespace PresentationLayer.Helpers
{
    public class GameHelpers
    {
        #region Singleton Pattern
        private static GameHelpers instance = null;

        private static readonly object lockObj = new object();

        public static GameHelpers Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new GameHelpers());
                }
            }
        }
        #endregion

        #region Public Methods
        public Enum CheckAndCreateGame(string name, string developer, string rating, string date, string categories, string description, out int newId)
        {
            newId = -1;

            if (!Utils.StringIsValid(name))
                return EnAddGame.invalidName;

            if (!Utils.StringIsValid(developer))
                return EnAddGame.invalidDeveloper;

            if (!Utils.StringIsValid(rating))
                return EnAddGame.invalidRating;

            if (!Utils.StringIsValid(date) || !DateTime.TryParse(date, out _))
                return EnAddGame.invalidDate;

            if (!Utils.StringIsValid(categories))
                return EnAddGame.invalidCategories;

            if (!Utils.StringIsValid(description))
                return EnAddGame.invalidDescription;

            return GamesManager.Instance.CreateAndInsert(name, developer, rating, date, categories, description, out newId);
        }
        #endregion
    }
}