using PostTraining.Application.Common;
using PostTraining.Domain.Models;
using PostTraining.Factories;
using PostTraining.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PostTraining.Application.Handlers
{
    public class CartHandler
    {
        private CartRepository cartRepo = new CartRepository();
        private CartFactory cartFactory = new CartFactory();

        public Response<CartItem> AddToCart(String userId, String productId, int quantity)
        {
            CartItem existing = cartRepo.GetCartItem(userId, productId);

            if (existing != null)
            {
                existing.Quantity += quantity;
                cartRepo.UpdateCartItem(existing);

                return new Response<CartItem>()
                {
                    Success = true,
                    Message = "Cart item updated",
                    Payload = existing
                };
            }

            CartItem newItem = cartFactory.CreateCartItem(userId, productId, quantity);
            cartRepo.AddCartItem(newItem);

            return new Response<CartItem>()
            {
                Success = true,
                Message = "Cart item created",
                Payload = newItem
            };
        }

        public Response<List<CartItem>> GetCartByUserId(String userId)
        {
            List<CartItem> items = cartRepo.GetCartByUserId(userId);

            return new Response<List<CartItem>>()
            {
                Success = true,
                Message = "Cart retreived",
                Payload = items
            };
        }

        public Response<CartItem> GetCartItem(String userId, String productId)
        {
            CartItem item = cartRepo.GetCartItem(userId, productId);

            return new Response<CartItem>()
            {
                Success = true,
                Message = "Cart item retreived",
                Payload = item
            };
        }

        public Response<Boolean> ReduceItemAmount(String userId, String productId)
        {
            CartItem existing = cartRepo.GetCartItem(userId, productId);

            if (existing == null)
            {
                return new Response<Boolean>()
                {
                    Success = false,
                    Message = "Item not found in cart.",
                    Payload = false
                };
            }

            if (existing.Quantity >= 1)
            {
                existing.Quantity -= 1;

                if (existing.Quantity == 0)
                {
                    return RemoveFromCart(userId, productId);
                }
                else
                {
                    cartRepo.UpdateCartItem(existing);
                    return new Response<Boolean>()
                    {
                        Success = true,
                        Message = "Reduced quantity by 1",
                        Payload = true
                    };
                }

            }

            return RemoveFromCart(userId, productId);
        }

        public Response<Boolean> IncreaseItemAmount(String userId, String productId)
        {
            CartItem existing = cartRepo.GetCartItem(userId, productId);

            existing.Quantity += 1;
            cartRepo.UpdateCartItem(existing);

            return new Response<Boolean>()
            {
                Success = true,
                Message = "Increased quantity by 1",
                Payload = true
            };
        }

        public Response<Boolean> RemoveFromCart(String userId, String productId)
        {
            cartRepo.RemoveCartItem(userId, productId);
            return new Response<Boolean>()
            {
                Success = true,
                Message = "Item removed",
                Payload = true
            };
        }
    }
}