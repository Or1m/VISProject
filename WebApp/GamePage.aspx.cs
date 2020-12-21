using BusinessLayer.BusinessObjects;
using BusinessLayer.Controllers;
using PresentationLayer;
using PresentationLayer.Enums;
using PresentationLayer.Helpers;
using System;
using System.Web.UI;

namespace WebApp
{
    public partial class GamePage : Page
    {
        private Game game;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Default lastPage = (Default)Context.Handler;

                game = GamesManager.Instance.LoadGame(Default.Games[lastPage.SelectedIndex].Id);
                Application["gameId"] = game.Id;
                Application["gameDate"] = game.ReleaseDate;

                FillBoxes();
            }
        }

        private void FillBoxes()
        {
            textbox1.Text = game.Name;
            textbox2.Text = game.Description;
            textbox3.Text = game.Developer;
            textbox4.Text = game.Rating;
            textbox5.Text = game.ReleaseDate.ToString();
            textbox6.Text = game.AverageUserScore.ToString();
            textbox7.Text = game.AverageReviewerScore.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            EnReleaseDate state = Utils.CompareDate(DateTime.Parse(Application["gameDate"].ToString()));

            if (state == EnReleaseDate.notReleased)
            {
                MsgBox("You cannot add review to unreleased game. If you want to be notified when the game comes out add it to favorite", Page, this);
            }

            else if (state == EnReleaseDate.oldLessThan24)
                MsgBox("Due to protection against review bombing, you cannot review the game yet. " +
                    "Try to spend more time with game.", Page, this);

            else
                Server.Transfer("AddReviewPage.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            EnFavorite status = ActorHelpers.Instance.CheckAndAddToFavorite(int.Parse(Application["userId"].ToString()), int.Parse(Application["gameId"].ToString()));

            if (status == EnFavorite.sucessfullyAdded)
                MsgBox("Successfully added to list of your favorite games", Page, this);

            else if (status == EnFavorite.somethingWentWrong)
                MsgBox("Something went wrong, try it later please", Page, this);

            else
                MsgBox("Game is already in list of your favorite games", Page, this);
        }

        public void MsgBox(string ex, Page pg, object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
    }
}