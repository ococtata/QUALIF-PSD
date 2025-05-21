using PostTraining.Application.Common;
using PostTraining.Controllers;
using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostTraining.Views.Auth
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            label_error.Text = "";
        }

        protected void lbutton_login_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Auth/LoginPage.aspx");
        }

        protected void button_register_Click(object sender, EventArgs e)
        {
            String name = textbox_name.Text;
            String email = textbox_email.Text;
            String password = textbox_password.Text;

            UserController userController = new UserController();

            Response<User> resp = userController.Register(name, email, password);

            if (!resp.Success)
            {
                label_error.ForeColor = System.Drawing.Color.Red;
                label_error.Text = resp.Message;
                return;
            }

            label_error.ForeColor = System.Drawing.Color.Green;
            label_error.Text = resp.Message + ", redirecting to Login Page...";

            Response.Redirect("~/Views/Auth/LoginPage.aspx");
        }
    }
}