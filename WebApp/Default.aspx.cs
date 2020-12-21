using BusinessLayer.BusinessObjects;
using BusinessLayer.Controllers;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace WebApp
{
    public partial class Default : Page
    {
        #region Fields and Properties
        private User loggedUser;

        private static List<Game> games;
        public static List<Game> Games { get => games; }

        private int selectedIndex;
        public int SelectedIndex { get => selectedIndex; }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            Button2.Enabled = false;

            if (!IsPostBack)
            {
                UpdateGames();
            }
        }

        #region Event Handlers
        protected void Button1_Click(object sender, EventArgs e)
        {
            loggedUser = (User)ActorHelpers.Instance.LoadActor(textbox1.Text, false);

            if(loggedUser != null)
            {
                labelResults.Text = "Logged in";
                labelResults.ForeColor = System.Drawing.Color.Green;
                Button2.Enabled = true;

                Application["userId"] = loggedUser.Id;
            }
            else
            {
                labelResults.Text = "Invalid input";
                labelResults.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            selectedIndex = DropDownList1.SelectedIndex;
            Server.Transfer("GamePage.aspx");
        }
        #endregion

        private void UpdateGames()
        {
            games = GamesManager.Instance.LoadGamesHeadersWithCategories();
            DropDownList1.DataSource = games;
            DropDownList1.DataBind();
        }
    }
}