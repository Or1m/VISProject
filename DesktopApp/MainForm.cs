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
        //List<Game> games = new List<Game>();
        //User user;
        public MainForm()
        {
            InitializeComponent();

            buttLogin.Enabled = false;
            button1.Visible = false;
        }

        private void ButtConnect_Click(object sender, EventArgs e)
        {
            //Database db = new Database();
            //bool connected = false;
            //try
            //{
            //    connected = db.Connect();
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    labelConnect.ForeColor = Color.Red;
            //    labelConnect.Text = "Error";
            //}

            //if(connected)
            //{
            //    labelConnect.ForeColor = Color.Green;
            //    labelConnect.Text = "Connected";

            //    refreshGames();
            //}
        }

        private void refreshGames()
        {
            //GameTable gameTable = new GameTable();
            //games = gameTable.selectGamesWithCategories();
            //dataGridGames.DataSource = games;


            for (int i = 0; i < dataGridGames.Rows.Count; i++)
            {
                string cat = "";
                //foreach (Category c in games[i].Categories)
                //{
                //    cat += c.ToStringHeader() + ", ";
                //}
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

                //FormGame fg = new FormGame(gameIndex, games[selectedrowindex].Categories, user);
                //fg.Show();
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
