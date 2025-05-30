﻿using PostTrainingFrontend.Controllers;
using PostTrainingFrontend.Models.Common;
using PostTrainingFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostTrainingFrontend.Views.Admin
{
    public partial class UpdateProductPage : System.Web.UI.Page
    {
        private ProductController productController = new ProductController();
        private UserController userController = new UserController();
        protected void Page_Load(object sender, EventArgs e)
        {
            label_error.Text = "";

            textbox_price.Attributes["step"] = "0.1";

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

            User currentUser = Session["user"] as User;

            if (!currentUser.Role.Equals("admin"))
            {
                Response.Redirect("~/Views/Common/HomePage.aspx");
                return;
            }

            if (!IsPostBack)
            {
                String id = Request["Id"];
                Response<Product> respProduct = productController.GetProduct(id);

                if (respProduct.Success)
                {
                    Product product = respProduct.Payload;
                    textbox_id.Text = product.Id.ToString();
                    textbox_name.Text = product.Name;
                    textbox_description.Text = product.Desc;
                    textbox_price.Text = product.Price.ToString();
                    textbox_tier.Text = product.Tier.ToString();
                    textbox_type.Text = product.Type.ToString();
                    textbox_stock.Text = product.Stock.ToString();
                }
                else
                {
                    Response.Redirect("~/Views/Common/HomePage.aspx");
                    return;
                }
            }
        }

        protected void button_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Common/HomePage.aspx");
        }

        protected void button_update_Click(object sender, EventArgs e)
        {
            String id = textbox_id.Text;
            String name = textbox_name.Text;
            String desc = textbox_description.Text;
            float price = float.Parse(textbox_price.Text);
            String type = textbox_type.Text;
            int tier = int.Parse(textbox_tier.Text);
            int stock = int.Parse(textbox_stock.Text);

            Response<Product> resp = productController.UpdateProduct(id, name, tier, price, desc, type, stock);

            if (!resp.Success)
            {
                label_error.ForeColor = System.Drawing.Color.Red;
                label_error.Text = resp.Message;
                return;
            }

            label_error.ForeColor = System.Drawing.Color.Green;
            label_error.Text = resp.Message + ", redirecting to Home Page...";

            Response.Redirect("~/Views/Common/HomePage.aspx");
        }
    }
}