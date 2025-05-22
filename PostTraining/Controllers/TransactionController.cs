using PostTraining.Application.Common;
using PostTraining.Application.Handlers;
using PostTraining.Domain.DTO;
using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Controllers
{
    public class TransactionController
    {
        CartController cartController = new CartController();
        TransactionHandler transactionHandler = new TransactionHandler();

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

            transactionHandler.CreateTransaction(userId, cartResp.Payload);
            
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
            Response<List<TransactionHistoryViewModel>> resp = transactionHandler.GetUserTransactions(userId);

            if(!resp.Success)
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