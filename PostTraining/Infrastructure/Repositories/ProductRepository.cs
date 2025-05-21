using PostTraining.Domain.Models;
using PostTraining.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Infrastructure.Repositories
{
    public class ProductRepository
    {
        private LocalDatabaseEntities1 db = Database.GetInstance();

        public List<Product> GetProducts()
        {
            List<Product> products = db.Products.ToList();

            return products;
        }

        public Product GetProductById(String id)
        {
            Guid guidId;
            if (!Guid.TryParse(id, out guidId))
            {
                return null;
            }

            Product product = db.Products.Find(guidId);
            if (product == null) return null;

            return product;
        }

        public Product GetProductByName(String name)
        {
            Product product = db.Products.FirstOrDefault(u => u.Name == name);
            if (product == null) return null;

            return product;
        }

        public void CreateProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public Product UpdateProduct(Product product)
        {

            Product updateProduct = db.Products.Find(product.Id);

            if (updateProduct == null) return null;

            updateProduct.Name = product.Name;
            updateProduct.Tier = product.Tier;
            updateProduct.Desc = product.Desc;
            updateProduct.Type = product.Type;
            updateProduct.Stock = product.Stock;

            db.SaveChanges();

            return updateProduct;
        }

        public bool DeleteProduct(String id)
        {
            Guid guidId;
            if (!Guid.TryParse(id, out guidId))
            {
                return false;
            }

            Product product = db.Products.Find(guidId);
            if (product == null) return false;


            db.Products.Remove(product);

            db.SaveChanges();
            return true;
        }
    }
}