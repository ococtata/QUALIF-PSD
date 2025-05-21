using PostTraining.Application.Common;
using PostTraining.Controllers;
using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostTraining.Views
{
    public partial class HomePage : System.Web.UI.Page
    {
        private User currentUser = null;

        private ProductController productController = new ProductController();
        private UserController userController = new UserController();
        private CartController cartController = new CartController();

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

            if(!IsPostBack) RefreshProductGridView();

            System.Diagnostics.Debug.WriteLine("Current user is: " + currentUser);

            if (currentUser.Role.Equals("admin"))
            {
                gridview_product.Columns[7].Visible = true;
                header.InnerText = "Home Page Admin";
            }
            else
            {
                gridview_product.Columns[8].Visible = true;
                header.InnerText = "Home Page Customer";
            }
        }

        public void RefreshProductGridView()
        {
            Response<List<Product>> resp = productController.GetProducts();

            if (resp.Success)
            {
                gridview_product.DataSource = resp.Payload;
                gridview_product.DataBind();
            }
            else
            {
                label_error.ForeColor = System.Drawing.Color.Red;
                label_error.Text = resp.Message;
            }
        }

        protected void gridview_product_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gridview_product.Rows[e.RowIndex];
            String id = row.Cells[0].Text;

            Response<Boolean> resp = productController.DeleteProduct(id);

            if (resp.Success)
            {
                RefreshProductGridView();
            }
            else
            {
                label_error.ForeColor = System.Drawing.Color.Red;
                label_error.Text = resp.Message;
            }
        }

        protected void gridview_product_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gridview_product.Rows[e.NewEditIndex];
            String id = row.Cells[0].Text;

            Response.Redirect("~/Views/Admin/UpdateProductPage.aspx?Id=" + id);
        }

        protected void gridview_product_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gridview_product.Rows[index];

                string productId = row.Cells[0].Text;

                TextBox quantityTextbox = (TextBox)row.FindControl("textbox_quantity");
                System.Diagnostics.Debug.WriteLine("Quantity textbox value: " + quantityTextbox.Text);


                int quantity = 0;

                if (!int.TryParse(quantityTextbox.Text, out quantity) || quantity < 1)
                {
                    quantity = 0;
                }

                Response<CartItem> resp = cartController.AddCartItem(currentUser.Id.ToString(), productId, quantity);

                if (resp.Success)
                {
                    RefreshProductGridView();
                }
                else
                {
                    label_error.ForeColor = System.Drawing.Color.Red;
                    label_error.Text = resp.Message;
                }
            }
        }
    }
}