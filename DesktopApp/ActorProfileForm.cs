using BusinessLayer.BusinessObjects;
using BusinessLayer.BusinessObjects.BaseObjects;
using System;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class ActorProfileForm : Form
    {
        private Actor actor;
        public ActorProfileForm()
        {
            InitializeComponent();
        }

        public ActorProfileForm(Actor actor) :this()
        {
            this.actor = actor;
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            //string workingDirectory = Environment.CurrentDirectory;
            //string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

            //pictureBox1.ImageLocation = projectDirectory + @"\resources\" + user.Gender + ".jpg";

            textBox1.Text = actor.FirstName;
            textBox2.Text = actor.LastName;
            //textBox3.Text = user.Nick; // nahrad workom
            textBox4.Text = actor.Gender.ToString();
            textBox5.Text = actor.Country;
            textBox6.Text = actor.DateOfBirth.ToString("dd/MM/yyyy");
            textBox7.Text = actor.RegistrationDate.ToString("dd/MM/yyyy");

            //textBox8.Text = user.FavoriteCategory.Name;


            //UserReviewTable userReviewTable = new UserReviewTable();
            //GameTable gameTable = new GameTable();
            //ReviewerTable reviewerTable = new ReviewerTable();

            //List<User_review> reviews = userReviewTable.selectReviewsForUser(user.User_id);

            //foreach (User_review ur in reviews)
            //{
            //    reviewBox.Text += ur.ToString() + Environment.NewLine;
            //}

            //List<Game> favGames = gameTable.selectFavoritGames(user.User_id);

            //foreach (Game g in favGames)
            //{
            //    textBox9.Text += g.ToStringHeader() + Environment.NewLine;
            //}

            //List<Reviewer> favRevs = reviewerTable.selectFavoritReviewers(user.User_id);

            //foreach (Reviewer r in favRevs)
            //{
            //    textBox10.Text += r.ToStringHeader() + Environment.NewLine;
            //}
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //int rows = 0;

            //if (textBox1.ReadOnly == true)
            //{
            //    MessageBox.Show("You are now in editing mode \nsave changes by click to edit button again");
            //}

            //else
            //{
            //    try
            //    {
            //        UserTable userTable = new UserTable();
            //        user.First_name = textBox1.Text;
            //        user.Last_name = textBox2.Text;
            //        user.Gender = char.Parse(textBox4.Text);
            //        user.Country = textBox5.Text;
            //        user.Date_of_birth = DateTime.Parse(textBox6.Text);

            //        rows = userTable.updateUser(user);
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Something went wrong");
            //    }

            //    if(rows > 0)
            //        MessageBox.Show("Changes saved");
            //    else
            //        MessageBox.Show("Nothing to change");
            //}


            //foreach (TextBox textbox in this.Controls.OfType<TextBox>())
            //{
            //    if(textbox.Name != "textBox8" && textbox.Name != "textBox7" && textbox.Name != "textBox3")
            //        textbox.ReadOnly = !textbox.ReadOnly;
            //}
        }
    }
}
