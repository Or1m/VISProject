using BusinessLayer.Controllers;
using DTO;
using System;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class DailyStatisticsViewForm : Form
    {
        public DailyStatisticsViewForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime val = dateTimePicker1.Value;
            string selectedDateString = val.Day.ToString() + "." + val.Month.ToString() + "." + val.Year.ToString();

            if (!GeneralManager.Instance.TryToFindDaily(selectedDateString, out DailyStatisticsDTO dto))
            {
                if (MessageBox.Show("Record not found") == DialogResult.OK)
                    FillTextBoxes(string.Empty, string.Empty, string.Empty);
            }
            else
            {
                textBox1.Text = dto.NumberOfUserReviews.ToString();
                textBox2.Text = dto.NumberOfReviewerReviews.ToString();
                textBox3.Text = dto.Date.ToString();

                FillTextBoxes(dto.NumberOfUserReviews.ToString(), dto.NumberOfReviewerReviews.ToString(), dto.Date.ToString());
            }
        }

        private void FillTextBoxes(string userRews, string reviewerRews, string timestamp)
        {
            textBox1.Text = userRews;
            textBox2.Text = reviewerRews;
            textBox3.Text = timestamp;
        }
    }
}
