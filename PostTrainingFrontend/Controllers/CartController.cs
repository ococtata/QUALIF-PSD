using PostTrainingFrontend.CartWebService;
using PostTrainingFrontend.Models;
using PostTrainingFrontend.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTrainingFrontend.Controllers
{
    public class CartController
    {
        private String err = "";

        private CartWebService.CartWebService cartWB = new CartWebService.CartWebService();
        private ProductWebService.ProductWebService productWB = new ProductWebService.ProductWebService();

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

            String jsonResponse = cartWB.GetCartByUserId(userId);
            return Json.Decode<Response<List<CartItem>>>(jsonResponse);
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

            String jsonProd = productWB.GetProduct(productId);
            Product existing = Json.Decode<Response<Product>>(jsonProd).Payload;

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

            String jsonCart = cartWB.AddToCart(userId, productId, quantity);
            return Json.Decode<Response<CartItem>>(jsonCart);
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

            String jsonResponse = cartWB.RemoveFromCart(userId, productId);
            return Json.Decode<Response<Boolean>>(jsonResponse);
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

            String jsonResponse = cartWB.ReduceItemAmount(userId, productId);
            return Json.Decode<Response<Boolean>>(jsonResponse);
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

            String jsonProd = productWB.GetProduct(productId);
            Product existing = Json.Decode<Response<Product>>(jsonProd).Payload;

            String jsonCart = cartWB.GetCartItem(userId, productId);
            CartItem cartItem = Json.Decode<Response<CartItem>>(jsonCart).Payload;

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

            String jsonResponse = cartWB.IncreaseItemAmount(userId, productId);
            return Json.Decode<Response<Boolean>>(jsonResponse);
        }

        public Response<Boolean> ClearCart(String userId)
        {
            if (userId.Equals(""))
            {
                return new Response<Boolean>()
                {
                    Success = false,
                    Message = "UserId is empty",
                    Payload = false,
                };
            }

            String jsonResponse = cartWB.ClearCart(userId);
            return Json.Decode<Response<Boolean>>(jsonResponse);
        }

    }
}