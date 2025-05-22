using PostTraining.Application.Common;
using PostTraining.Application.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PostTraining.WebServices
{
    /// <summary>
    /// Summary description for CartWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CartWebService : System.Web.Services.WebService
    {
        private CartHandler cartHandler = new CartHandler();

        [WebMethod]
        public String AddToCart(String userId, String productId, int quantity)
        {
            return Json.Encode(cartHandler.AddToCart(userId, productId, quantity));
        }

        [WebMethod]
        public String GetCartByUserId(String userId)
        {
            return Json.Encode(cartHandler.GetCartByUserId(userId));
        }

        [WebMethod]
        public String GetCartItem(String userId, String productId)
        {
            return Json.Encode(cartHandler.GetCartItem(userId, productId));
        }

        [WebMethod]
        public String ReduceItemAmount(String userId, String productId)
        {
            return Json.Encode(cartHandler.ReduceItemAmount(userId, productId));
        }

        [WebMethod]
        public String IncreaseItemAmount(String userId, String productId)
        {
            return Json.Encode(cartHandler.IncreaseItemAmount(userId, productId));
        }

        [WebMethod]
        public String RemoveFromCart(String userId, String productId)
        {
            return Json.Encode(cartHandler.RemoveFromCart(userId, productId));
        }

        [WebMethod]
        public String ClearCart(String userId)
        {
            return Json.Encode(cartHandler.ClearCart(userId));
        }
    }
}
