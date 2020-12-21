using BusinessLayer.BusinessObjects;
using BusinessLayer.Controllers;
using BusinessLayer.Enums;
using PresentationLayer.Enums;
using PresentationLayer.Helpers;
using System;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class RegisterReviewerForm : ValidatableForm
    {
        #region Private Fields
        private User oldUser;
        private string work;
        #endregion

        #region Constructors
        public RegisterReviewerForm()
        {
            InitializeComponent();
        }

        public RegisterReviewerForm(User user, string work) : this()
        {
            this.work = work;
            oldUser = user;
        }
        #endregion

        #region Private Event Handlers
        private void RegisterReviewerForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = oldUser.FirstName;
            textBox2.Text = oldUser.LastName;
            textBox3.Text = work;
            textBox4.Text = oldUser.Gender.ToString();
            textBox5.Text = oldUser.Country;
            textBox6.Text = oldUser.DateOfBirth.ToString("dd/MM/yyyy");
            textBox7.Text = oldUser.RegistrationDate.ToString("dd/MM/yyyy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnRequest status = GeneralHelpers.Instance.CheckRequest(textBox1.Text, textBox2.Text, textBox4.Text, textBox5.Text,
                textBox6.Text, textBox7.Text, textBox3.Text, out _, out _);

            ProcessEnRequest(status);

            if (status == EnRequest.valid)
            {
                DialogResult dialogResult = DialogResult.None;

                EnBusinessRequest req =  ActorsManager.Instance.CheckAndRegisterReviewer
                (
                    textBox1.Text, textBox2.Text, textBox4.Text, textBox5.Text,
                    textBox6.Text, textBox7.Text, textBox3.Text
                );

                ProcessEnBusinessRequest(req, ref dialogResult);

                if (dialogResult == DialogResult.OK)
                    Close();
            }  
        }
        #endregion
    }
}