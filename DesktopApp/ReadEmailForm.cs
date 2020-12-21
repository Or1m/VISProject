using BusinessLayer.BusinessObjects;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class ReadEmailForm : Form
    {
        #region Private Fields
        private User user;
        private string workOrMsg;
        private bool approved;
        #endregion


        #region Constructors
        public ReadEmailForm()
        {
            InitializeComponent();
        }

        public ReadEmailForm(User user, string workOrMsg, bool approved) : this()
        {
            this.user       = user;
            this.workOrMsg  = workOrMsg;
            this.approved   = approved;
        }
        #endregion

        #region Private Event Handlers
        private void ReadEmailForm_Load(object sender, EventArgs e)
        {
            if(approved)
            {
                label2.Text      = "Approved";
                button1.Text     = "Register account";
                label2.ForeColor = Color.Green;
            }
            else
            {
                button1.Text      = "Close";
                label2.Text       = "Rejected";
                label2.ForeColor  = Color.Red;
                richTextBox1.Text = workOrMsg;

                richTextBox1.Enabled = true;
                label3.Enabled       = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(approved)
                new RegisterReviewerForm(user, workOrMsg).Show();

            Close();
        }
        #endregion
    }
}