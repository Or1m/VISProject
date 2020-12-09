using BusinessLayer.Enums;
using PresentationLayer;
using PresentationLayer.Enums;
using System;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class AddGameForm : Form
    {
        public AddGameForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Enum status = GameHelpers.Instance.
                CheckAndCreateGame(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox7.Text, textBox8.Text);

            if (status is EnAddGame)
            { 
                switch(status)
                {
                    case EnAddGame.invalidName:
                        MessageBox.Show("Invalid name");
                        break;
                    case EnAddGame.invalidDeveloper:
                        MessageBox.Show("Invalid developer");
                        break;
                    case EnAddGame.invalidRating:
                        MessageBox.Show("Invalid rating");
                        break;
                    case EnAddGame.invalidDate:
                        MessageBox.Show("Invalid date");
                        break;
                    case EnAddGame.invalidCategories:
                        MessageBox.Show("Invalid categories");
                        break;
                    case EnAddGame.invalidDescription:
                        MessageBox.Show("Invalid description");
                        break;
                    case EnAddGame.valid:
                        MessageBox.Show("Validation successfull");
                        break;
                    default:
                        MessageBox.Show("Invalid name");
                        break;
                }
            }
            else if (status is EnCreateGame)
            {
                switch (status)
                {
                    case EnCreateGame.invalidRatingFormat:
                        MessageBox.Show("Invalid rating format");
                        break;
                    case EnCreateGame.invalidCategoriesFormat:
                        MessageBox.Show("Invalid categories format");
                        break;
                    case EnCreateGame.inserted:
                        MessageBox.Show("Successfully added");
                        break;
                    default:
                        MessageBox.Show("Something wrong");
                        break;
                }
            }
        }
    }
}