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
            
            if (status == EnAddReview.successfullyAdded)
                MessageBox.Show("Review successfully submitted");
            else if (status == EnAddReview.invalidInput)
                MessageBox.Show("Please fill title properly and don´t remember to select score and order");
            else
                MessageBox.Show("Something wrong");
        }
    }
}
