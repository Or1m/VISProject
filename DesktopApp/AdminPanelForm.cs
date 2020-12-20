
using BusinessLayer.BusinessObjects;
using BusinessLayer.Controllers;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class AdminPanelForm : Form
    {
        private Email<User, string, string> email;
        public AdminPanelForm()
        {
            InitializeComponent();
        }

        private void AdminPanelForm_Load(object sender, System.EventArgs e)
        {
            if(EmailManager.Instance.IsEmailForAdminInMailbox())
            {
                email = EmailManager.Instance.ReadLastEmailForAdmin();
            
                textBox1.Text = email.t.FirstName + " " + email.t.LastName;
                textBox2.Text = email.u;
                richTextBox1.Text = email.q;
            }

            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                richTextBox2.Enabled = false;
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            ProcessResult(SendEmail(textBox2.Text, true));
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            ProcessResult(SendEmail(richTextBox2.Text, false));
        }

        private bool SendEmail(string msg, bool approved)
        {
            try
            {
                EmailManager.Instance.SendEmailFromAdmin(email.t, msg, approved);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void ProcessResult(bool result)
        {
            if (result)
            {
                if (MessageBox.Show("Sended") == DialogResult.OK)
                    Close();
            }
            else
            {
                MessageBox.Show("Something wromg");
            }
        }
    }
}