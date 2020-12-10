using BusinessLayer.BusinessObjects;
using PresentationLayer.Helpers;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class AskForReviewerForm : Form
    {
        private User user;
        public AskForReviewerForm(User user)
        {
            InitializeComponent();

            this.user = user;
        }

        private void AskForReviewerForm_Load(object sender, System.EventArgs e)
        {
            textBox1.Text = user.FirstName;
            textBox2.Text = user.LastName;
            textBox4.Text = user.Gender.ToString();
            textBox5.Text = user.Country;
            textBox6.Text = user.DateOfBirth.ToString("dd/MM/yyyy");
            textBox7.Text = user.RegistrationDate.ToString("dd/MM/yyyy");
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            GeneralHelpers.Instance.CheckRequestAndSendFurther(textBox1.Text, textBox2.Text, textBox4.Text, textBox5.Text, 
            textBox6.Text, textBox7.Text, textBox3.Text, richTextBox1.Text);
        }
    }
}
