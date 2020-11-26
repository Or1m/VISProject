
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
    public partial class NewReview : Form
    {
        int userId;
        int gameId;
        public NewReview()
        {
            InitializeComponent();
        }

        public NewReview(int uId, int gId)
        {
            InitializeComponent();

            userId = uId;
            gameId = gId;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            bool hasTitle = false;


            if (!(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text)))
            {
                hasTitle = true;
            }

            int score = comboBox1.SelectedIndex + 1;
            int order = comboBox2.SelectedIndex + 1;

            int rows = 0;
            if(hasTitle && order > 0)
            {
                //UserReviewTable userReviewTable = new UserReviewTable();
                //User_review userReview = new User_review(textBox1.Text, score, userId, gameId, DateTime.Now, order);
                
                try
                {
                    //rows = userReviewTable.insertNew(userReview);
                }
                catch
                {
                    MessageBox.Show("Something went wrong");
                }
            }
            else
            {
                MessageBox.Show("Title or order is empty");
            }

            if(rows > 0)
                MessageBox.Show("Review successfully submitted");
        }
    }
}
