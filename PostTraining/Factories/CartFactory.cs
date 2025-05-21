using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Factories
{
    public class CartFactory
    {
        public CartItem CreateCartItem(String userId, String productId, int quantity)
        {
            return new CartItem
            {
                UserId = Guid.Parse(userId),
                ProductId = Guid.Parse(productId),
                Quantity = quantity
            };
        }
    }
}