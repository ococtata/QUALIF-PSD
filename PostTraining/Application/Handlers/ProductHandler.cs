using PostTraining.Application.Common;
using PostTraining.Domain.Models;
using PostTraining.Factories;
using PostTraining.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PostTraining.Application.Handlers
{
    public class ProductHandler
    {

        private ProductFactory productFactory = new ProductFactory();
        private ProductRepository productRepo = new ProductRepository();
        public Response<List<Product>> GetProducts()
        {
            List<Product> products = productRepo.GetProducts();

            return new Response<List<Product>>()
            {
                Success = true,
                Message = "Successfully got Products",
                Payload = products,
            };
        }

        public Response<Product> GetProduct(String id)
        {
            Product product = productRepo.GetProductById(id);

            if(product == null)
            {
                return new Response<Product>()
                {
                    Success = false,
                    Message = "Product with id " + id + " not found",
                    Payload = null,
                };
            }

            return new Response<Product>()
            {
                Success = true,
                Message = "Product with id " + id + " found",
                Payload = product,
            };
        }

        public Response<Product> CreateProduct(String name, int rarity, float price, String desc, String type, int stock)
        {
            String nameTrimmed = name.Trim();
            Product productDTO = productRepo.GetProductByName(nameTrimmed);

            if(productDTO != null)
            {
                return new Response<Product>()
                {
                    Success = false,
                    Message = "Product with name " + nameTrimmed + " already registered",
                    Payload = null,
                };
            };

            Product product = productFactory.CreateProduct(nameTrimmed, rarity, price, desc, type, stock);

            productRepo.CreateProduct(product);


            return new Response<Product>()
            {
                Success = true,
                Message = "Successfully added product with name: " + name,
                Payload = product,
            };
        }

        public Response<Product> UpdateProduct(String id, String name, int rarity, float price, String desc, String type, int stock)
        {
            Product product = productFactory.CreateProduct(name, rarity, price, desc, type, stock);

            Product updatedProduct = productRepo.UpdateProduct(product);

            if(updatedProduct == null)
            {
                return new Response<Product>()
                {
                    Success = false,
                    Message = "Product with id " + id + " not found",
                    Payload = null,
                };
            }

            return new Response<Product>()
            {
                Success = true,
                Message = "Successfully updated product with id: " + id,
                Payload = updatedProduct,
            };
        }

        public Response<Product> DeleteProduct(String id)
        {
            Boolean success = productRepo.DeleteProduct(id);

            if(!success)
            {
                return new Response<Product>()
                {
                    Success = false,
                    Message = "Product with id " + id + " not found",
                    Payload = null,
                };
            }

            return new Response<Product>()
            {
                Success = true,
                Message = "Successfully deleted product with id: " + id,
                Payload = null,
            };
        }
    }
}