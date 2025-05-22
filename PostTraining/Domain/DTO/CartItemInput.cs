using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Domain.DTO
{
    public class CartItemInput
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

    }
}