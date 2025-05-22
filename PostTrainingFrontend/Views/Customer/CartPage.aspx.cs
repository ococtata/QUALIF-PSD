using PostTrainingFrontend.Controllers;
using PostTrainingFrontend.Models.Common;
using PostTrainingFrontend.Models.DTO;
using PostTrainingFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostTrainingFrontend.Views.Customer
{
    public partial class CartPage : System.Web.UI.Page
    {
        private User currentUser = null;

        private ProductController productController = new ProductController();
        private UserController userController = new UserController();
        private CartController cartController = new CartController();
        private TransactionController transactionController = new TransactionController();
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

            if (!IsPostBack) RefreshCartGridView();
        }

        private void RefreshCartGridView()
        {
            Response<List<CartItem>> respCart = cartController.GetCartItems(currentUser.Id.ToString());

            if (!respCart.Success)
            {
                label_error.ForeColor = System.Drawing.Color.Red;
                label_error.Text = respCart.Message;
                return;
            }

            List<CartItemViewModel> viewModels = new List<CartItemViewModel>();

            float total = 0;
            foreach (CartItem item in respCart.Payload)
            {
                Response<Product> respProduct = productController.GetProduct(item.ProductId.ToString());

                if (respProduct.Success && respProduct.Payload != null)
                {
                    Product product = respProduct.Payload;

                    viewModels.Add(new CartItemViewModel
                    {
                        ProductId = product.Id.ToString(),
                        Name = product.Name,
                        Tier = product.Tier,
                        Type = product.Type,
                        Desc = product.Desc,
                        Price = (float)product.Price,
                        Quantity = item.Quantity
                    });

                    total += (float)(item.Quantity * product.Price);
                }
            }

            gridview_cart.DataSource = viewModels;
            gridview_cart.DataBind();

            label_total_amount.Text = "$" + total.ToString();
        }

        protected void gridview_cart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string productId = e.CommandArgument.ToString();

            if (productId.Equals("") || productId == null)
            {
                label_error.Text = "Invalid product ID.";
                return;
            }

            Response<Boolean> resp = null;

            if (e.CommandName == "Increase")
            {
                resp = cartController.IncreaseItemAmount(currentUser.Id.ToString(), productId);
            }
            else if (e.CommandName == "Decrease")
            {
                resp = cartController.ReduceItemAmount(currentUser.Id.ToString(), productId);
            }

            if (resp != null)
            {
                if (resp.Success)
                {
                    label_error.ForeColor = System.Drawing.Color.Green;
                    label_error.Text = "Cart updated successfully.";
                }
                else
                {
                    label_error.ForeColor = System.Drawing.Color.Red;
                    label_error.Text = resp.Message;
                }

                RefreshCartGridView();
            }
        }

        protected void button_order_Click(object sender, EventArgs e)
        {
            Response<Boolean> resp = transactionController.Order(currentUser.Id.ToString());

            if (resp.Success)
            {
                label_error.ForeColor = System.Drawing.Color.Green;
                label_error.Text = resp.Message;
                RefreshCartGridView();
            }
            else
            {
                label_error.ForeColor = System.Drawing.Color.Red;
                label_error.Text = resp.Message;
            }
        }
    }
}