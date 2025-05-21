using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Infrastructure.Database
{
    public class ProductSeeder
    {
        public static void Seed(LocalDatabaseEntities1 db)
        {
            if (!db.Products.Any())
            {
                db.Products.AddRange(new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Neural Accelerator",
                        Tier = 3,
                        Price = 299.99f,
                        Desc = "Boosts neural processing speed",
                        Type = "Implant",
                        Stock = 10
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cyberarm Mk II",
                        Tier = 4,
                        Price = 499.50f,
                        Desc = "Heavy-duty cybernetic arm",
                        Type = "Limb",
                        Stock = 5
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Ocular HUD",
                        Tier = 2,
                        Price = 149.00f,
                        Desc = "Real-time display HUD implant",
                        Type = "Eye",
                        Stock = 8
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Nano Healing Kit",
                        Tier = 1,
                        Price = 89.99,
                        Desc = "Self-administering nanobot healing",
                        Type = "Medical",
                        Stock = 15
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Reflex Booster",
                        Tier = 3,
                        Price = 329.99,
                        Desc = "Increases motor reflex response",
                        Type = "Enhancer",
                        Stock = 7
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cyberlegs v3",
                        Tier = 4,
                        Price = 455.75,
                        Desc = "Enhanced mobility cybernetic legs",
                        Type = "Limb",
                        Stock = 6
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cortical Stack",
                        Tier = 5,
                        Price = 899.00,
                        Desc = "Consciousness backup implant",
                        Type = "Neural",
                        Stock = 3
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Infrared Eye Lens",
                        Tier = 2,
                        Price = 179.00,
                        Desc = "Enables night and heat vision",
                        Type = "Eye",
                        Stock = 12
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Synthetic Heart",
                        Tier = 5,
                        Price = 750.00,
                        Desc = "High-end bio-mechanical heart",
                        Type = "Organ",
                        Stock = 2
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Hacking Neural Link",
                        Tier = 3,
                        Price = 399.99,
                        Desc = "Interface for direct cyber intrusion",
                        Type = "Implant",
                        Stock = 9
                    }
                });

                db.SaveChanges();
            }
        }
    }
}