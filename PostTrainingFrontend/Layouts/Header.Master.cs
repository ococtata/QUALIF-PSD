using PostTrainingFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostTrainingFrontend.Layouts
{
    public partial class Header : System.Web.UI.MasterPage
    {
        private String currentPage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            getCurrentPage();

            User user = Session["user"] as User;

            if (user == null)
            {
                Response.Redirect("~/Views/Auth/LoginPage.aspx");
                return;
            }

            if (user.Role.Equals("admin"))
            {
                button_add.Visible = true;
            }
            else
            {
                button_cart.Visible = true;
                button_history.Visible = true;
            }
        }

        protected void button_home_Click(object sender, EventArgs e)
        {
            if (!currentPage.EndsWith("homepage.aspx"))
            {
                Response.Redirect("~/Views/Common/HomePage.aspx");
            }
        }

        protected void button_add_Click(object sender, EventArgs e)
        {
            if (!currentPage.EndsWith("addproductpage.aspx"))
            {
                Response.Redirect("~/Views/Admin/AddProductPage.aspx");
            }
        }

        protected void button_cart_Click(object sender, EventArgs e)
        {
            if (!currentPage.EndsWith("cartpage.aspx"))
            {
                Response.Redirect("~/Views/Customer/CartPage.aspx");
            }
        }

        protected void button_logout_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["user_cookie"] != null)
            {
                HttpCookie cookie = new HttpCookie("user_cookie");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Views/Auth/LoginPage.aspx");
        }

        private void getCurrentPage()
        {
            currentPage = Request.Url.AbsolutePath.ToLower();
        }

        protected void button_history_Click(object sender, EventArgs e)
        {
            if (!currentPage.EndsWith("historypage.aspx"))
            {
                Response.Redirect("~/Views/Customer/HistoryPage.aspx");
            }
        }
    }
}