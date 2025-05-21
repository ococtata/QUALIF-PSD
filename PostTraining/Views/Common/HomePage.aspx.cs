using PostTraining.Application.Common;
using PostTraining.Controllers;
using PostTraining.Domain.Models;
using PostTraining.Infrastructure.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostTraining.Views
{
    public partial class HomePage : Middleware
    {
        private ProductController productController = new ProductController();
        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshProductGridView();
            OnLoad(e);

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
        }

        protected void gridview_product_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gridview_product.Rows[e.RowIndex];
            String id = row.Cells[0].Text;

            Response<Product> resp = productController.DeleteProduct(id);
        }

        protected void gridview_product_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gridview_product.Rows[e.NewEditIndex];
            String id = row.Cells[0].Text;

            Response.Redirect("~/Views/Admin/UpdateProductPage.aspx?Id=" + id);
        }

        protected void gridview_product_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}