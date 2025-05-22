using PostTraining.Application.Common;
using PostTraining.Application.Handlers;
using PostTraining.Domain.DTO;
using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PostTraining.WebServices
{
    /// <summary>
    /// Summary description for TransactionWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TransactionWebService : System.Web.Services.WebService
    {

        private Application.Handlers.TransactionHandler transactionHandler = new Application.Handlers.TransactionHandler();

        [WebMethod]
        public String GetUserTransactions(String userId)
        {
            return Json.Encode(transactionHandler.GetUserTransactions(userId));
        }

        [WebMethod]
        public String CreateTransaction(String userId, CartItemInput[] cartItems)
        {
            List<CartItem> cartList = cartItems.Select(dto => new CartItem
            {
                UserId = Guid.Parse(dto.UserId),
                ProductId = Guid.Parse(dto.ProductId),
                Quantity = dto.Quantity
            }).ToList();

            return Json.Encode(transactionHandler.CreateTransaction(userId, cartList));
        }
    }
}
