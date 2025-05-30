﻿using PostTrainingFrontend.Models;
using PostTrainingFrontend.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTrainingFrontend.Controllers
{
    public class ProductController
    {
        private ProductWebService.ProductWebService productWB = new ProductWebService.ProductWebService();
        private String err = "";

        public Response<List<Product>> GetProducts()
        {
            String jsonResponse = productWB.GetProducts();
            return Json.Decode<Response<List<Product>>>(jsonResponse);    
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

            String jsonResponse = productWB.GetProduct(id);
            return Json.Decode<Response<Product>>(jsonResponse);
        }
        public Response<Product> CreateProduct(String name, int tier, float price, String desc, String type, int stock)
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

            String jsonResponse = productWB.CreateProduct(name, tier, price, desc, type, stock);
            return Json.Decode<Response<Product>>(jsonResponse);
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
            
            String jsonResponse = productWB.UpdateProduct(id, name, tier, price, desc, type, stock);
            return Json.Decode<Response<Product>>(jsonResponse);
        }

        public Response<Boolean> DeleteProduct(String id)
        {
            if (id.Equals(""))
            {
                err = "Input cannot be empty";
            }

            if (!err.Equals(""))
            {
                return new Response<Boolean>
                {
                    Success = false,
                    Message = err,
                    Payload = false
                };
            }

            String jsonResponse = productWB.DeleteProduct(id);
            return Json.Decode<Response<Boolean>>(jsonResponse);
        }

        public Response<Boolean> ReduceProductStock(String productId, int amount)
        {
            if (productId.Equals("") || amount.ToString().Equals(""))
            {
                return new Response<Boolean>()
                {
                    Success = false,
                    Message = "Input cannot be empty",
                    Payload = false,
                };
            }

            String jsonResponse = productWB.ReduceProductStock(productId, amount);
            return Json.Decode<Response<Boolean>>(jsonResponse);
        }
    }
}