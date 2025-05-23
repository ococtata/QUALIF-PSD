using PostTrainingFrontend.Controllers;
using PostTrainingFrontend.Models.Common;
using PostTrainingFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PostTrainingFrontend.Datasets;
using PostTrainingFrontend.Models.DTO;
using PostTrainingFrontend.Reports;

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
                TransactionReport report = new TransactionReport();
                CrystalReportViewer1.ReportSource = report;
                report.SetDataSource(GetDataSet(resp.Payload));
            }
            else
            {
                label_error.Text = resp.Message;
            }
        }

        private TransactionDataset GetDataSet(List<TransactionHistoryViewModel> transactions)
        {
            TransactionDataset dataset = new TransactionDataset();
            var transactionTable = dataset.Transaction;
            var detailTable = dataset.TransactionDetail;

            foreach ( var transaction in transactions)
            {
                var transactionTableRow = transactionTable.NewRow();
                transactionTableRow["Id"] = transaction.TransactionId;
                transactionTableRow["OrderDate"] = transaction.OrderDate;
                transactionTable.Rows.Add(transactionTableRow);

                foreach ( var i in transaction.Items)
                {
                    var detailTableRow = detailTable.NewRow();
                    detailTableRow["TransactionId"] = transaction.TransactionId;
                    detailTableRow["ProductId"] = i.ProductId;
                    detailTableRow["Name"] = i.ProductName;
                    detailTableRow["Tier"] = i.Tier;
                    detailTableRow["Price"] = i.Price;
                    detailTableRow["Desc"] = i.Desc;
                    detailTableRow["Type"] = i.Type;
                    detailTableRow["Quantity"] = i.Quantity;
                    detailTable.Rows.Add(detailTableRow);

                }
            }

            return dataset;
        }
    }
}