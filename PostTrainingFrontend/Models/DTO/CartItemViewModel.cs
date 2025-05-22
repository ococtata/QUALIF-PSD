using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTrainingFrontend.Models.DTO
{
    public class CartItemViewModel
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int Tier { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}