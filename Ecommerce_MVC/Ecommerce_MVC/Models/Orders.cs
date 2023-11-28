using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce_MVC.Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public Nullable<int> ProductId { get; set; }
    }
}