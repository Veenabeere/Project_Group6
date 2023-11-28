using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce_MVC.Models
{
    public class ShoppingCart
    {
        public int CartId { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }

    }
}