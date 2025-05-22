using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Factories
{
    public class TransactionFactory
    {
        public Transaction createTransaction(String userId, List<CartItem> cart)
        {
            Guid id = Guid.NewGuid();
            return new Transaction()
            {
                Id = id,
                UserId = Guid.Parse(userId),
                OrderDate = DateTime.Now,
                TransactionDetails = cart.Select(item => new TransactionDetail()
                {
                    TransactionId = id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                }).ToList(),
            };
        }
    }
}