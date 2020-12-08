using BusinessLayer.BusinessObjects;
using BusinessLayer.BusinessObjects.BaseObjects;
using BusinessLayer.Controllers;
using PresentationLayer;
using PresentationLayer.Enums;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class FormGame : Form
    {
        private Game game;
        private Actor actor;

        public FormGame(Actor actor)
        {
            InitializeComponent();

            this.actor = actor;
        }

        public FormGame(int gameIndex, List<Category> categories, Actor actor) : this(actor)
        {
            game = GamesManager.Instance.LoadGame(gameIndex);
            
            Text = game.Name;
            game.Categories = categories;
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
                MessageBox.Show("Due to protection against review bombing, you cannot review the game yet. Try to spend more time with game.");

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

            //string workingDirectory = Environment.CurrentDirectory;
            //string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

            //pictureBox1.ImageLocation = projectDirectory + @"\resources\" + game.Game_id + ".jpg";

            //Routines routines = new Routines();

            //for (int i = 0; i < 10; i++)
            //{
            //    Tuple<string, int> tuple = routines.lastTenReviews(game.Game_id);
            //    if (tuple.Item1 == "null")
            //    {
            //        textBox9.Text = game.Name + Environment.NewLine + "does not have enough reviews";
            //        break;
            //    }
            //    else
            //        textBox9.Text += tuple.Item1 + " " + tuple.Item2 + Environment.NewLine;
            //}
        }
    }
}