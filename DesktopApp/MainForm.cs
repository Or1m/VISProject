using BusinessLayer.BusinessObjects;
using BusinessLayer.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class MainForm : Form
    {
        private List<Game> games;

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
            List<Game> games = GamesManager.Instance.LoadGamesHeadersWithCategories();
            dataGridGames.DataSource = games;

            for (int i = 0; i < dataGridGames.Rows.Count; i++)
            {
                string cat = string.Empty;
                foreach (Category c in games[i].Categories)
                    cat += c.ToStringHeader() + ", ";

                cat = cat.Remove(cat.Length - 2);

                dataGridGames.Rows[i].Cells["Categories"].Value = cat;
            }
        }

        private void DataGridGames_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridGames.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridGames.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridGames.Rows[selectedrowindex];
                int gameIndex = int.Parse(selectedRow.Cells["gameId"].Value.ToString());

                new FormGame(gameIndex, games[selectedrowindex].Categories, null).Show();
            }
        }

        private void ButtLogin_Click(object sender, EventArgs e)
        {
            //UserTable userTable = new UserTable();

            //if (!(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text)))
            //{
            //    user = userTable.selectUserByNickWithCategory(textBox1.Text);
            //}

            //if (user is null)
            //{
            //    label2.ForeColor = Color.Red;
            //    label2.Text = "Invalid name";
            //}
            //else
            //{
            //    label2.ForeColor = Color.Green;
            //    label2.Text = "Logged in";
            //}
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
            //UserProfile userProfile = new UserProfile(user);
            //userProfile.Show();
        }
    }
}
