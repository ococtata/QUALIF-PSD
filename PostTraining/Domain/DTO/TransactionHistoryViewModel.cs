using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Domain.DTO
{
    public class TransactionHistoryViewModel
    {
        public string TransactionId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<TransactionItemViewModel> Items { get; set; }
    }

    public class TransactionItemViewModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}