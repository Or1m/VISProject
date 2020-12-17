using BusinessLayer.BusinessObjects;
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
    public partial class ReadEmailForm : Form
    {
        private User user;
        private string work;
        private bool approved;

        public ReadEmailForm()
        {
            InitializeComponent();
        }

        public ReadEmailForm(User user, string work, bool approved)
        {
            this.user = user;
            this.work = work;
            this.approved = approved;
        }

        private void ReadEmailForm_Load(object sender, EventArgs e)
        {
            if(approved)
            {
                label2.Text = "Approved";
                label2.ForeColor = Color.Green;
                button1.Text = "Register account";
            }
            else
            {
                label2.Text = "Rejected";
                label2.ForeColor = Color.Red;
                button1.Text = "Close";

                richTextBox1.Enabled = true;
                label3.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(approved)
            {
                new RegisterReviewerForm(user, work).Show();
            }
            else
            {
                Close();
            }
        }
    }
}
