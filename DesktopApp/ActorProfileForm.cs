using BusinessLayer.BusinessObjects;
using BusinessLayer.BusinessObjects.BaseObjects;
using BusinessLayer.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class ActorProfileForm : Form
    {
        private Actor actor;
        private bool isUser;

        private List<Review> rews;

        public ActorProfileForm()
        {
            InitializeComponent();
        }

        public ActorProfileForm(Actor actor) :this()
        {
            this.actor = actor;
            isUser = actor is User;

            button1.Visible = isUser;
            button2.Visible = isUser;
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = actor.Gender == 'F' ? new Bitmap(Properties.Resources.F) : new Bitmap(Properties.Resources.M);

            textBox1.Text = actor.FirstName;
            textBox2.Text = actor.LastName;
            textBox3.Text = isUser ? ((User)actor).Nick : "---";
            textBox4.Text = actor.Gender.ToString();
            textBox5.Text = actor.Country;
            textBox6.Text = actor.DateOfBirth.ToString("dd/MM/yyyy");
            textBox7.Text = actor.RegistrationDate.ToString("dd/MM/yyyy");

            textBox8.Text = isUser ? ((User)actor).FavoriteCategory.Name : "---";

            if(isUser)
                rews = ActorsManager.Instance.LoadReviewsForUser(actor.Id);

            foreach (var r in rews)
            {
                reviewBox.Text += r.ToString() + Environment.NewLine;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new AskForReviewerForm((User)actor).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(EmailManager.Instance.EmailsFromAdmin.Count > 0)
            {
                Email<User, string, bool> email = EmailManager.Instance.EmailsFromAdmin.Peek();

                if (email.t.Id == actor.Id)
                {
                    EmailManager.Instance.EmailsFromAdmin.Dequeue();
                }

                new ReadEmailForm(email.t, email.u, email.q).Show();
            }
        }
    }
}
