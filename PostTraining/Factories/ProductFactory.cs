using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Factories
{
    public class ProductFactory
    {
        public Product CreateProduct(String name, int tier, float price, String desc, String type, int stock)
        {
            Guid id = Guid.NewGuid();

            Product product = new Product()
            {
                Id = id,
                Name = name,
                Tier = tier,
                Price = price,
                Desc = desc,
                Type = type,
                Stock = stock
            };

            return product;
        }
    }
}