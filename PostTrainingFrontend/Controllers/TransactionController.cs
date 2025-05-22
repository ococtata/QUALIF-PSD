using PostTrainingFrontend.Models;
using PostTrainingFrontend.Models.Common;
using PostTrainingFrontend.Models.DTO;
using PostTrainingFrontend.TransactionWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTrainingFrontend.Controllers
{
    public class TransactionController
    {
        private CartController cartController = new CartController();
        private TransactionWebService.TransactionWebService transWB = new TransactionWebService.TransactionWebService();

        public Response<Boolean> Order(String userId)
        {
            Response<List<CartItem>> cartResp = cartController.GetCartItems(userId);

            if (cartResp.Payload.Count == 0)
            {
                return new Response<Boolean>()
                {
                    Success = false,
                    Message = "Order failed, user dont have any items in cart",
                    Payload = false,
                };
            }

            CartItemInput[] cartItemInput = cartResp.Payload.Select(ci => new CartItemInput
            {
                ProductId = ci.ProductId.ToString(),
                UserId = userId,
                Quantity = ci.Quantity
            }).ToArray();

            String jsonResponse = transWB.CreateTransaction(userId, cartItemInput);
            Response<Transaction> transactionResponse = Json.Decode<Response<Transaction>>(jsonResponse);
            
            if(!transactionResponse.Success)
            {
                return new Response<Boolean>()
                {
                    Success = false,
                    Message = "Order failed: " + transactionResponse.Message,
                    Payload = false,
                };
            }

            cartController.ClearCart(userId);

            return new Response<Boolean>()
            {
                Success = true,
                Message = "Order successful",
                Payload = true,
            };
        }
        public Response<List<TransactionHistoryViewModel>> GetTransactionHistory(String userId)
        {

            String jsonResponse = transWB.GetUserTransactions(userId);
            Response<List<TransactionHistoryViewModel>> resp = Json.Decode<Response<List<TransactionHistoryViewModel>>>(jsonResponse);

            if (!resp.Success)
            {
                return new Response<List<TransactionHistoryViewModel>>()
                {
                    Success = false,
                    Message = "Failed to get history for userId: " + userId,
                    Payload = null,
                };
            }

            return resp;
        }

    }
}