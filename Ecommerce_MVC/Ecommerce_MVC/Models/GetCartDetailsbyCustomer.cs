using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce_MVC.Models
{
    public class GetCartDetailsbyCustomer
    {
        public int CategoryId { get; set; }
        public string ModelNumber { get; set; }

        public string ModelName { get; set; }

        public string ProductImage { get; set; }
        public decimal UnitCost { get; set; }
        public string Description { get; set; }

        public int CartId { get; set; }

        public int Quantity { get; set; }
    }
}