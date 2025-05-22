using PostTraining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Infrastructure.Repositories
{
    public class TransactionRepository
    {
        private LocalDatabaseEntities1 db = Database.GetInstance();

        public List<Transaction> GetUserTransactions(String userId)
        {
            List<Transaction> transactions = db.Transactions.Include("TransactionDetails.Product")
                .Where(t => t.UserId.ToString() == userId).OrderByDescending(t => t.OrderDate)
                .ToList();

            return transactions;
        }

        public void CreateTransaction(Transaction transaction)
        {
            db.Transactions.Add(transaction);
            db.SaveChanges();
        }
    }
}