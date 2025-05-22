using PostTrainingFrontend.Controllers;
using PostTrainingFrontend.Models.Common;
using PostTrainingFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostTrainingFrontend.Views.Auth
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            label_error.Text = "";

            if (Session["user"] != null)
            {
                Response.Redirect("~/Views/Common/HomePage.aspx");
                return;
            }
        }
        protected void button_login_Click(object sender, EventArgs e)
        {
            String email = textbox_email.Text;
            String password = textbox_password.Text;
            Boolean remember = checkbox_remember.Checked;

            UserController userController = new UserController();
            Response<User> resp = userController.Login(email, password);

            if (!resp.Success)
            {
                label_error.ForeColor = System.Drawing.Color.Red;
                label_error.Text = resp.Message;
                return;
            }

            label_error.ForeColor = System.Drawing.Color.Green;
            label_error.Text = resp.Message + ", redirecting...";

            if (remember)
            {
                HttpCookie cookie = new HttpCookie("user_cookie");
                cookie.Value = CookieHelper.Encrypt(resp.Payload.Id.ToString());
                cookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(cookie);
            }

            Session["user"] = resp.Payload;

            Response.Redirect("~/Views/Common/HomePage.aspx");
        }

        protected void lbutton_register_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Auth/RegisterPage.aspx");
        }
    }
}