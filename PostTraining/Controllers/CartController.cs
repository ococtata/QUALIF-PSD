using PostTraining.Application.Common;
using PostTraining.Application.Handlers;
using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Controllers
{
    public class CartController
    {
        private String err = "";
        private CartHandler cartHandler = new CartHandler();
        private ProductHandler productHandler = new ProductHandler();

        public Response<List<CartItem>> GetCartItems(String userId)
        {
            if (userId.Equals(""))
            {
                return new Response<List<CartItem>>()
                {
                    Success = false,
                    Message = "User ID cannot be empty",
                    Payload = null,
                };
            }

            return cartHandler.GetCartByUserId(userId);
        }

        public Response<CartItem> AddCartItem(String userId, String productId, int quantity)
        {
            if (userId.Equals("") || productId.Equals("") || quantity.ToString().Equals(""))
            {
                err = "Inputs cannot be empty";
            }

            if (quantity <= 0)
            {
                err = "Quantity must at least be 1";
            }

            Product existing = productHandler.GetProduct(productId).Payload;

            if (existing == null)
            {
                err = "Product with id " + productId + " not found";
            }

            if (existing.Stock < quantity)
            {
                err = "Requested quantity exceeded product\'s stock";
            }

            if (!err.Equals(""))
            {
                return new Response<CartItem>()
                {
                    Success = false,
                    Message = err,
                    Payload = null,
                };
            }

            return cartHandler.AddToCart(userId, productId, quantity);
        }

        public Response<Boolean> RemoveFromCart(String userId, String productId)
        {
            if (userId.Equals("") || productId.Equals(""))
            {
                return new Response<Boolean>()
                {
                    Success = false,
                    Message = "Input cannot be empty",
                    Payload = false,
                };
            }

            return cartHandler.RemoveFromCart(userId, productId);
        }

        public Response<Boolean> ReduceItemAmount(String userId, String productId)
        {
            if (userId.Equals("") || productId.Equals(""))
            {
                return new Response<Boolean>()
                {
                    Success = false,
                    Message = "Input cannot be empty",
                    Payload = false,
                };
            }

            return cartHandler.ReduceItemAmount(userId, productId);
        }
        public Response<Boolean> IncreaseItemAmount(String userId, String productId)
        {
            if (userId.Equals("") || productId.Equals(""))
            {
                return new Response<Boolean>()
                {
                    Success = false,
                    Message = "Input cannot be empty",
                    Payload = false,
                };
            }

            Product existing = productHandler.GetProduct(productId).Payload;
            CartItem cartItem = cartHandler.GetCartItem(userId, productId).Payload;

            if (existing == null)
            {
                err = "Product with id " + productId + " not found";
            }
            else if (cartItem == null)
            {
                err = "Cart item for product " + productId + " not found";
            }
            else if (cartItem.Quantity >= existing.Stock)
            {
                err = "Cannot add more. Stock limit reached.";
            }

            if (!err.Equals(""))
            {
                return new Response<Boolean>()
                {
                    Success = false,
                    Message = err,
                    Payload = false,
                };
            }

            return cartHandler.IncreaseItemAmount(userId, productId);
        }

    }
}