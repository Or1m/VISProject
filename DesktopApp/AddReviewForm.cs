using PresentationLayer;
using PresentationLayer.Enums;
using System;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class AddReviewForm : Form
    {
        private int userId;
        private int gameId;

        public AddReviewForm()
        {
            InitializeComponent();
        }

        public AddReviewForm(int uId, int gId) : this()
        {
            userId = uId;
            gameId = gId;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int score = comboBox1.SelectedIndex + 1;
            int order = comboBox2.SelectedIndex + 1;

            EnAddReview status = ReviewHelpers.Instance.CheckAndCreateReview(textBox1.Text, score, userId, gameId, DateTime.Now, order);

            bool closeAfter = false;

            if (status == EnAddReview.invalidTitle)
                MessageBox.Show("Please fill title properly");

            else if (status == EnAddReview.invalidScore)
                MessageBox.Show("Score has to be more than zero");

            else if (status == EnAddReview.invalidOrder)
                MessageBox.Show("Order has to be more than zero");

            else if(status == EnAddReview.successfullyAdded)
            {
                MessageBox.Show("Review successfully submitted");
                closeAfter = true;
            }
            else
            {
                MessageBox.Show("Something wrong");
                closeAfter = true;
            }

            if (closeAfter)
                Close();
        }
    }
}
