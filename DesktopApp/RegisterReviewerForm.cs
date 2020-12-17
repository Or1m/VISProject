using BusinessLayer.BusinessObjects;
using BusinessLayer.Controllers;
using BusinessLayer.Enums;
using PresentationLayer.Enums;
using PresentationLayer.Helpers;
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
    public partial class RegisterReviewerForm : Form
    {
        private User oldUser;
        private string work;
        
        public RegisterReviewerForm()
        {
            InitializeComponent();
        }

        public RegisterReviewerForm(User user, string work) : this()
        {
            this.work = work;
            oldUser = user;
        }

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
            //TODO dorobit checky
            EnRequest result = GeneralHelpers.Instance.CheckRequst(textBox1.Text, textBox2.Text, textBox4.Text, textBox5.Text,
                textBox6.Text, textBox7.Text, textBox3.Text, out _, out _);

            if (result == EnRequest.valid)
            {
                EnBusinessRequest req =  ActorsManager.Instance.CheckAndRegisterReviewer
                (
                    textBox1.Text, textBox2.Text, textBox4.Text, textBox5.Text,
                    textBox6.Text, textBox7.Text, textBox3.Text
                );

                if (req == EnBusinessRequest.sucess)
                    MessageBox.Show("Added");
            }  
        }
    }
}
