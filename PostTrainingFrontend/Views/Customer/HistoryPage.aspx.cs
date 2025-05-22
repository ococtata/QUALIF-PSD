using PostTrainingFrontend.Controllers;
using PostTrainingFrontend.Models.Common;
using PostTrainingFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostTrainingFrontend.Views.Customer
{
    public partial class HistoryPage : System.Web.UI.Page
    {
        private User currentUser = null;

        private UserController userController = new UserController();
        protected void Page_Load(object sender, EventArgs e)
        {
            label_error.Text = "";

            if (Session["user"] == null && Request.Cookies["user_cookie"] == null)
            {
                Response.Redirect("~/Views/Auth/LoginPage.aspx");
                return;
            }

            if (Session["user"] == null)
            {
                String cookie = Request.Cookies["user_cookie"].Value;

                Response<User> resp = userController.LoginUserByCookie(cookie);

                if (!resp.Success)
                {
                    Request.Cookies["user_cookie"].Expires = DateTime.Now.AddDays(-1);
                    Response.Redirect("~/Views/Auth/LoginPage.aspx");
                    return;
                }

                Session["user"] = resp.Payload;
            }

            currentUser = Session["user"] as User;

            if (!IsPostBack) LoadTransactionHistory();
        }

        private void LoadTransactionHistory()
        {
            TransactionController controller = new TransactionController();
            var resp = controller.GetTransactionHistory(currentUser.Id.ToString());

            if (resp.Success)
            {
                repeater_transactions.DataSource = resp.Payload;
                repeater_transactions.DataBind();
            }
            else
            {
                label_error.Text = resp.Message;
            }
        }
    }
}