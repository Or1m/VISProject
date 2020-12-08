using BusinessLayer.BusinessObjects;
using PresentationLayer;
using BusinessLayer.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.BusinessObjects.BaseObjects;

namespace DesktopApp
{
    public partial class MainForm : Form
    {
        private List<Game> games;
        private Actor actor;

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
                labelConnect.ForeColor = Color.Red;
                labelConnect.Text = "An error occur";
            }
            else
            {
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
                DataGridViewRow selectedRow = dataGridGames.Rows[selectedrowindex];
                int gameIndex = int.Parse(selectedRow.Cells["Id"].Value.ToString());

                new FormGame(gameIndex, games[selectedrowindex].Categories, actor).Show();
            }
        }

        private void ButtLogin_Click(object sender, EventArgs e)
        {
            actor = ActorHelpers.Instance.LoadActor(textBox1.Text, checkBoxRev.Checked);

            if (actor is null)
            {
                label2.ForeColor = Color.Red;
                label2.Text = "Invalid name";
            }
            else
            {
                label2.ForeColor = Color.Green;
                label2.Text = "Logged in";
            }
        }

        private void LabelConnect_TextChanged(object sender, EventArgs e)
        {
            if (labelConnect.Text == "Connected")
                buttLogin.Enabled = true;
        }

        private void Label2_TextChanged(object sender, EventArgs e)
        {
            if(label2.Text == "Logged in")
            {
                button1.Visible = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            new ActorProfileForm(actor).Show();
        }
    }
}
