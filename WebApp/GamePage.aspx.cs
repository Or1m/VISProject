using BusinessLayer.BusinessObjects;
using BusinessLayer.Controllers;
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
            Server.Transfer("AddReviewPage.aspx");
        }
    }
}