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
    /// Summary description for ProductWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProductWebService : System.Web.Services.WebService
    {
        private ProductHandler productHandler = new ProductHandler();

        [WebMethod]
        public String GetProducts()
        {
            return Json.Encode(productHandler.GetProducts());
        }

        [WebMethod]
        public String GetProduct(String id)
        {
            return Json.Encode(productHandler.GetProduct(id));
        }

        [WebMethod]
        public String CreateProduct(String name, int tier, float price, String desc, String type, int stock)
        {
            return Json.Encode(productHandler.CreateProduct(name, tier, price, desc, type, stock));
        }

        [WebMethod]
        public String UpdateProduct(String id, String name, int tier, float price, String desc, String type, int stock)
        {
            return Json.Encode(productHandler.UpdateProduct(id, name, tier, price, desc, type, stock));
        }

        [WebMethod]
        public String DeleteProduct(String id)
        {
            return Json.Encode(productHandler.DeleteProduct(id));
        }

        [WebMethod]
        public String ReduceProductStock(String productId, int amount)
        {
            return Json.Encode(productHandler.ReduceProductStock(productId, amount));
        }
    }
}
