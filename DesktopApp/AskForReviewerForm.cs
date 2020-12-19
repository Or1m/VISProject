using BusinessLayer.BusinessObjects;
using BusinessLayer.Enums;
using PresentationLayer.Enums;
using PresentationLayer.Helpers;
using System;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class AskForReviewerForm : ValidatableForm
    {
        private User user;
        public AskForReviewerForm(User user)
        {
            InitializeComponent();

            this.user = user;
        }

        private void AskForReviewerForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = user.FirstName;
            textBox2.Text = user.LastName;
            textBox4.Text = user.Gender.ToString();
            textBox5.Text = user.Country;
            textBox6.Text = user.DateOfBirth.ToString("dd/MM/yyyy");
            textBox7.Text = user.RegistrationDate.ToString("dd/MM/yyyy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = DialogResult.None;

            Enum status = GeneralHelpers.Instance.CheckRequestAndSendFurther(textBox1.Text, textBox2.Text, textBox4.Text, textBox5.Text, 
            textBox6.Text, textBox7.Text, textBox3.Text, richTextBox1.Text, user.Id);

            if (status is EnRequest)
                ProcessEnRequest((EnRequest)status);

            else if(status is EnBusinessRequest)
                ProcessEnBusinessRequest((EnBusinessRequest)status, ref dialogResult);

            if (dialogResult == DialogResult.OK)
                Close();
        }
    }
}
