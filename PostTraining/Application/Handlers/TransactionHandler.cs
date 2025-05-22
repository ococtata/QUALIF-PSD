using PostTraining.Application.Common;
using PostTraining.Domain.DTO;
using PostTraining.Domain.Models;
using PostTraining.Factories;
using PostTraining.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Application.Handlers
{
    public class TransactionHandler
    {
        private TransactionRepository transRepo = new TransactionRepository();
        private TransactionFactory transFactory = new TransactionFactory();

        private ProductHandler productHandler = new ProductHandler();

        public Response<List<TransactionHistoryViewModel>> GetUserTransactions(String userId)
        {
            List<Transaction> transactions = transRepo.GetUserTransactions(userId);

            List<TransactionHistoryViewModel> transactionHistories = transactions.Select(t => new TransactionHistoryViewModel()
            {
                TransactionId = t.Id.ToString(),
                OrderDate = t.OrderDate,
                Items = t.TransactionDetails.Select(td => new TransactionItemViewModel()
                {
                    ProductName = td.Product.Name,
                    Quantity = td.Quantity,
                    Price = (float)td.Product.Price,
                }).ToList(),
            }).ToList();

            return new Response<List<TransactionHistoryViewModel>>()
            {
                Success = true,
                Message = "Retreived transaction history for userId: " + userId,
                Payload = transactionHistories
            };
        }

        public Response<Transaction> CreateTransaction(String userId, List<CartItem> cart)
        {
            Transaction transaction = transFactory.createTransaction(userId, cart);
            transRepo.CreateTransaction(transaction);

            foreach (CartItem item in cart)
            {
                Response<Boolean> resp = productHandler.ReduceProductStock(item.ProductId.ToString(), item.Quantity);

                if (!resp.Success)
                {
                    return new Response<Transaction>()
                    {
                        Success = false,
                        Message = "Failed to reduce stock: " + resp.Message,
                        Payload = null,
                    };
                }
            }

            return new Response<Transaction>()
            {
                Success = true,
                Message = "Transaction created",
                Payload = transaction
            };
        }
    }
}