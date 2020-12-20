using PresentationLayer.Enums;
using PresentationLayer.Helpers;
using System;
using System.Web.UI;

namespace WebApp
{
    public partial class AddReviewPage : System.Web.UI.Page
    {
        private static int[] validScores = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private static int[] validOrders = { 1, 2, 3, 4, 5};

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DropDown2.DataSource = validScores;
                DropDown2.DataBind();
                DropDown3.DataSource = validOrders;
                DropDown3.DataBind();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int score = DropDown2.SelectedIndex + 1;
            int order = DropDown3.SelectedIndex + 1;

            int gameId = int.Parse(Application["gameId"].ToString());
            int userId = int.Parse(Application["userId"].ToString());
            EnAddReview status = ReviewHelpers.Instance.CheckAndCreateReview(textbox1.Text, score, userId, gameId, DateTime.Now, order);

            bool closeAfter = false;

            if (status == EnAddReview.invalidTitle)
                MsgBox("Please fill title properly", Page, this);

            else if (status == EnAddReview.invalidScore)
                MsgBox("Score has to be more than zero", Page, this);

            else if (status == EnAddReview.invalidOrder)
                MsgBox("Order has to be more than zero", Page, this);

            else if (status == EnAddReview.successfullyAdded)
            {
                MsgBox("Review successfully submitted", Page, this);
                closeAfter = true;
            }
            else
            {
                MsgBox("Something wrong", Page, this);
            }

            if (closeAfter)
                Server.Transfer("Default.aspx");
        }

        public void MsgBox(string ex, Page pg, object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
    }
}