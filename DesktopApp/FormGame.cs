using BusinessLayer.BusinessObjects;
using BusinessLayer.BusinessObjects.BaseObjects;
using BusinessLayer.Controllers;
using PresentationLayer;
using PresentationLayer.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class FormGame : Form
    {
        private Game game;
        private Actor actor;

        private FormGame()
        {
            InitializeComponent();
        }

        public FormGame(int gameId, List<Category> categories, Actor actor) : this()
        {
            game = GamesManager.Instance.LoadGame(gameId);
            this.actor = actor;

            Text = game.Name;

            if (categories != null)
                game.Categories = categories;
            else
                game.Categories = GamesManager.Instance.LoadCategoriesForGame(gameId);
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            if (actor is null || actor is Reviewer)
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }

            FillBoxes();
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            AddGameToFavorite();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            EnReleaseDate state =  Utils.CompareDate((DateTime)game.ReleaseDate);

            if(state == EnReleaseDate.notReleased) 
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to be notified when the game comes out?", 
                    "You cannot add review to unreleased game", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                    AddGameToFavorite();
            }

            else if (state == EnReleaseDate.oldLessThan24)
                MessageBox.Show("Due to protection against review bombing, you cannot review the game yet. " +
                    "Try to spend more time with game.");

            else
                new AddReviewForm(actor.Id, game.Id).Show();
        }

        private void AddGameToFavorite()
        {
            EnFavorite status = ActorHelpers.Instance.CheckAndAddToFavorite(actor.Id, game.Id);

            if (status == EnFavorite.sucessfullyAdded)
                MessageBox.Show("Successfully added to list of your favorite games");

            else if(status == EnFavorite.somethingWentWrong)
                MessageBox.Show("Something went wrong, try it later please");

            else
                MessageBox.Show("Game is already in list of your favorite games");
        }

        private void FillBoxes()
        {
            button2.Select();
            button2.Text = "Add " + game.Name + " to your favorite games";

            textBox1.Text = game.Name;
            textBox2.Text = game.Developer;
            textBox3.Text = game.Rating;
            textBox4.Text = ((DateTime)game.ReleaseDate).Date.ToString("dd/MM/yyyy");
            textBox5.Text = game.AverageUserScore.ToString();
            textBox6.Text = game.AverageReviewerScore.ToString();
            textBox7.Text = game.ToStringCategories();
            textBox8.Text = game.Description;

            pictureBox1.Image = new Bitmap(Properties.Resources.astro);
        }
    }
}