using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce_MVC.Models
{
    public class customer
    {
        public int CustomerId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email ID")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$",
        ErrorMessage = "Invalid Email")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

      
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{10})$", ErrorMessage = "Entered phone format is not valid.")]
        [Display(Name = "MobileNumber")]
        public string PhoneNumber { get; set; }
    }
}