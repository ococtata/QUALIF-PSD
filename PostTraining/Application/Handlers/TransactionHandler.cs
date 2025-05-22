using PostTraining.Application.Common;
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

        public Response<List<Transaction>> GetTransactions()
        {
            List<Transaction> transactions = transRepo.GetTransactions();

            if (transactions == null)
            {
                return new Response<List<Transaction>>()
                {
                    Success = true,
                    Message = "No transactions found",
                    Payload = null
                };
            }

            return new Response<List<Transaction>>()
            {
                Success = true,
                Message = "Transactions found",
                Payload = transactions
            };
        }
        
        public Response<Transaction> CreateTransaction(String userId, List<CartItem> cart)
        {
            Transaction transaction = transFactory.createTransaction(userId, cart);
            transRepo.CreateTransaction(transaction);

            return new Response<Transaction>()
            {
                Success = true,
                Message = "Transaction created",
                Payload = transaction
            };
        }
    }
}