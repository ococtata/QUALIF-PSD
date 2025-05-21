using PostTraining.Application.Common;
using PostTraining.Controllers;
using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace PostTraining.Infrastructure.Middleware
{
    public class Middleware : Page
    {
        protected static User currentUser;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            System.Diagnostics.Debug.WriteLine("Middleware running");
            if (Session["user"] == null)
            {
                HttpCookie cookie = Request.Cookies["user_cookie"];
                if (cookie != null)
                {
                    UserController userController = new UserController();
                    Response<User> resp = userController.LoginUserByCookie(cookie.Value);

                    if (!resp.Success)
                    {
                        Request.Cookies["user_cookie"].Expires = DateTime.Now.AddDays(-1);
                        Response.Redirect("~/Views/Auth/LoginPage.aspx");
                        return;
                    }

                    User user = resp.Payload;

                    if (user != null)
                    {
                        Session["user"] = user;
                    }
                    else
                    {
                        Response.Redirect("~/Views/Auth/LoginPage.aspx");
                        return;
                    }
                }
                else
                {
                    Response.Redirect("~/Views/Auth/LoginPage.aspx");
                    return;
                }
            }

            currentUser = Session["user"] as User;
        }
    }
}