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
        private ProductFactory productFactory = new ProductFactory();

        public List<Product> GetProducts()
        {
            List<Product> products = db.Products.ToList();

            return products;
        }

        public Product GetProductById(String id)
        {
            Product product = db.Products.Find(id);
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
            Product product = db.Products.Find(id);
            if (product == null) return false;


            db.Products.Remove(product);

            db.SaveChanges();
            return true;
        }
    }
}