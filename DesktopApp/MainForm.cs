using BusinessLayer.BusinessObjects;
using BusinessLayer.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.BusinessObjects.BaseObjects;
using PresentationLayer.Helpers;

namespace DesktopApp
{
    public partial class MainForm : Form
    {
        private List<Game> games;
        private Actor actor;

        private bool connected;
        private bool loggedIn;
        private bool isReviewer;

        public MainForm()
        {
            InitializeComponent();

            buttLogin.Enabled = false;
            button1.Visible = false;
        }

        private void ButtConnect_Click(object sender, EventArgs e)
        {
            if (!BusinessLayer.Routines.IsConnected())
            {
                connected = false;

                labelConnect.ForeColor = Color.Red;
                labelConnect.Text = "An error occur";
            }
            else
            {
                connected = true;

                labelConnect.ForeColor = Color.Green;
                labelConnect.Text = "Connected";

                UpdateGames();
            }
        }

        private void UpdateGames()
        {
            games = GamesManager.Instance.LoadGamesHeadersWithCategories();
            dataGridGames.DataSource = games;

            for (int i = 0; i < dataGridGames.Rows.Count; i++)
                dataGridGames.Rows[i].Cells["Categories"].Value = games[i].ToStringCategories();
        }

        private void DataGridGames_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridGames.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridGames.SelectedCells[0].RowIndex;
                new FormGame(games[selectedrowindex].Id, games[selectedrowindex].Categories, actor).Show();
            }
        }

        private void ButtLogin_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "admin" || textBox1.Text == "Admin" || textBox1.Text == "Root" || textBox1.Text == "root")
            {
                new AdminPanelForm().Show();
                return;
            }


            actor = ActorHelpers.Instance.LoadActor(textBox1.Text, checkBoxRev.Checked);
            isReviewer = actor is Reviewer;

            if (actor is null)
            {
                loggedIn = false;

                label2.ForeColor = Color.Red;
                label2.Text = "Invalid name";
            }
            else
            {
                loggedIn = true;

                label2.ForeColor = Color.Green;
                label2.Text = "Logged in as " + (isReviewer ? "reviewer" : "user");
            }

            if (isReviewer)
            {
                buttonAdd.Visible = true;
                button2.Visible   = true;
                button3.Visible   = true;
            }
            else
            {
                buttonAdd.Visible = false;
                button2.Visible   = false;
                button3.Visible   = false;
            }
        }

        private void LabelConnect_TextChanged(object sender, EventArgs e)
        {
            buttLogin.Enabled = connected;
        }

        private void Label2_TextChanged(object sender, EventArgs e)
        {
            button1.Visible = loggedIn || isReviewer;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            new ActorProfileForm(actor).Show();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new AddGameForm().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GeneralManager.Instance.ExportDaily();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new DailyStatisticsViewForm().Show();
        }
    }
}
