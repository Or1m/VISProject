using BusinessLayer.Enums;
using PresentationLayer.Enums;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class ValidatableForm : Form
    {
        public ValidatableForm()
        {
            InitializeComponent();
        }


        #region Protected Static Void Methods
        protected static void ProcessEnRequest(EnRequest status)
        {
            switch (status)
            {
                case EnRequest.invalidFName:
                    MessageBox.Show("Invalid first name");
                    break;
                case EnRequest.invalidLName:
                    MessageBox.Show("Invalid last name");
                    break;
                case EnRequest.invalidCountry:
                    MessageBox.Show("Invalid country");
                    break;
                case EnRequest.invalidGender:
                    MessageBox.Show("Invalid Gender");
                    break;
                case EnRequest.invalidDateOfBirth:
                    MessageBox.Show("Invalid date of birth");
                    break;
                case EnRequest.invalidRegistrationDate:
                    MessageBox.Show("Invalid registration date");
                    break;
                case EnRequest.invalidWork:
                    MessageBox.Show("Invalid work");
                    break;
                case EnRequest.invalidWhyMe:
                    MessageBox.Show("Invalid Why me");
                    break;
                case EnRequest.valid:
                    break;
                default:
                    MessageBox.Show("Something wrong");
                    break;
            }
        }

        protected static void ProcessEnBusinessRequest(EnBusinessRequest status, ref DialogResult dialogResult)
        {
            switch (status)
            {
                case EnBusinessRequest.dateMismatch:
                    MessageBox.Show("Date missmatch");
                    break;
                case EnBusinessRequest.sucess:
                    dialogResult = MessageBox.Show("Request sended");
                    break;
                default:
                    MessageBox.Show("Something wrong");
                    break;
            }
        }
        #endregion
    }
}