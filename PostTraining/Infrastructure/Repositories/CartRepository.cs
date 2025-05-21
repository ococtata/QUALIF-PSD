using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;

namespace PostTraining.Infrastructure.Repositories
{
    public class CartRepository
    {
        private LocalDatabaseEntities1 db = Database.GetInstance();

        public List<CartItem> GetCartByUserId(String userId)
        {

            return db.CartItems.Where(c => c.UserId.ToString() == userId).ToList();
        }

        public CartItem GetCartItem(String userId, String productId)
        {
            return db.CartItems.FirstOrDefault(c => c.UserId.ToString() == userId &&
            c.ProductId.ToString() == productId);
        }

        public void AddCartItem(CartItem cartItem)
        {
            db.CartItems.Add(cartItem);
            db.SaveChanges();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            CartItem item = GetCartItem(cartItem.UserId.ToString(), cartItem.ProductId.ToString());

            if (item != null)
            {
                item.Quantity = cartItem.Quantity;
                db.SaveChanges();
            }
        }

        public Boolean RemoveCartItem(String userId, String productId)
        {
            CartItem item = GetCartItem(userId, productId);
            if (item == null) return false;

            db.CartItems.Remove(item);
            db.SaveChanges();

            return true;

        }

        public Boolean ClearCart(String userId)
        {
            List<CartItem> items = GetCartByUserId(userId);

            if (items == null) return false;

            db.CartItems.RemoveRange(items);
            db.SaveChanges();

            return true;
        }
    }
}