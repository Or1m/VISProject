using BusinessLayer.BusinessObjects;
using BusinessLayer.Controllers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class FormGame : Form
    {
        private Game game;
        private User user;

        public FormGame()
        {
            InitializeComponent();
        }

        public FormGame(int gameIndex, List<Category> categories, User user) : this()
        {
            game = GamesManager.Instance.LoadGame(gameIndex);
            this.user = user;

            Text = game.Name;
            game.Categories = categories;
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            if (user is null)
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }

            button2.Select();

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

            button2.Text = "Add " + game.Name + " to your favorite games";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //        FavoritGameTable favoritGameTable = new FavoritGameTable();
            //        try
            //        {
            //            if (favoritGameTable.insertNew(user.User_id, game.Game_id) == 1)
            //            {
            //                MessageBox.Show("Successfully added");
            //            }
            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show("Already in your favorite games");
            //        }
            //    }
        }
            private void Button1_Click(object sender, EventArgs e)
            {
                new AddReviewForm(user.Id, game.Id).Show();
            }
        }
    }