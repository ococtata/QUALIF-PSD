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
        private String err = "";

        public Response<List<Product>> GetProducts()
        {
            return productHandler.GetProducts();
        }

        public Response<Product> GetProduct(String id)
        {
            if (id.Equals(""))
            {
                err = "Input cannot be empty";
            }

            if (!err.Equals(""))
            {
                return new Response<Product>
                {
                    Success = false,
                    Message = err,
                    Payload = null
                };
            }

            return productHandler.GetProduct(id);
        }
        public Response<Product> CreateProduct(String name, int tier, float price, String desc, String type, int stock)
        {
            if(name.Equals("") ||  tier.ToString().Equals("") || price.ToString().Equals("") || desc.Equals("") || type.Equals("") || stock.ToString().Equals(""))
            {
                err = "Input cannot be empty";
            }

            if (tier < 1 || tier > 5)
            {
                err = "Tier must be between 1 and 5";
            }

            if (stock < 0)
            {
                err = "Stock cannot be below 0.";
            }

            if(!err.Equals(""))
            {
                return new Response<Product>
                {
                    Success = false,
                    Message = err,
                    Payload = null
                };
            }

            return productHandler.CreateProduct(name, tier, price, desc, type, stock); 
        }

        public Response<Product> UpdateProduct(String id, String name, int tier, float price, String desc, String type, int stock)
        {
            if (name.Equals("") || tier.ToString().Equals("") || price.ToString().Equals("") || desc.Equals("") || type.Equals("") || stock.ToString().Equals(""))
            {
                err = "Input cannot be empty";
            }

            if (tier < 1 || tier > 5)
            {
                err = "Tier must be between 1 and 5";
            }

            if (stock < 0)
            {
                err = "Stock cannot be below 0.";
            }

            if (!err.Equals(""))
            {
                return new Response<Product>
                {
                    Success = false,
                    Message = err,
                    Payload = null
                };
            }

            return productHandler.UpdateProduct(id, name, tier, price, desc, type, stock);
        }

        public Response<Product> DeleteProduct(String id)
        {
            if(id.Equals(""))
            {
                err = "Input cannot be empty";
            }

            if (!err.Equals(""))
            {
                return new Response<Product>
                {
                    Success = false,
                    Message = err,
                    Payload = null
                };
            }

            return productHandler.DeleteProduct(id);
        }
    }
}