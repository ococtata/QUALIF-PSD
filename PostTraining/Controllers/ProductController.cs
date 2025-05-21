using PostTraining.Application.Common;
using PostTraining.Application.Handlers;
using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Controllers
{
    public class ProductController
    {        
        private ProductHandler productHandler = new ProductHandler();

        public Response<List<Product>> GetProducts()
        {
            return productHandler.GetProducts();
        }

        public Response<Product> GetProduct(String id)
        {
            return productHandler.GetProduct(id);
        }
        public Response<Product> CreateProduct(String name, int tier, float price, String desc, String type, int stock)
        {
            return productHandler.CreateProduct(name, tier, price, desc, type, stock); 
        }

        public Response<Product> UpdateProduct(String id, String name, int tier, float price, String desc, String type, int stock)
        {
            return productHandler.UpdateProduct(id, name, tier, price, desc, type, stock);
        }

        public Response<Product> DeleteProduct(String id)
        {
            return productHandler.DeleteProduct(id);
        }
    }
}