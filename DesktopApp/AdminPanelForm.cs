
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
            var newEmails = EmailManager.Instance.EmailsToAdmin;

            if(newEmails.Count > 0)
            {
                email = newEmails.Dequeue();
            
                textBox1.Text = email.t.FirstName + " " + email.t.LastName;
                textBox2.Text = email.u;
                richTextBox1.Text = email.q;
            }

            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Email<User, string, bool> newEmail = 
                new Email<User, string, bool>(email.t, textBox2.Text, true);

            EmailManager.Instance.EmailsFromAdmin.Enqueue(newEmail);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            //TODO Send reject email
        }
    }
}
