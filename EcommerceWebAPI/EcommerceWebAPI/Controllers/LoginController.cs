using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EcommerceWebAPI.Models;

namespace EcommerceWebAPI.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {

        EcommerceDBContext db = new EcommerceDBContext();
        [HttpPost]
        public IHttpActionResult Login([FromBody] Customer loginCustomer)
        {
            // Add authentication logic here
            Customer customer = db.Customers.FirstOrDefault(x => x.EmailAddress == loginCustomer.EmailAddress && x.Password == loginCustomer.Password);
            if (customer != null)
            {
                // Authentication successful
                // You can generate a token or set a session variable to indicate the user is logged in
                return Ok("Login successful");
            }
            else
            {
                // Authentication failed
                return BadRequest("Invalid email address or password");
            }
        }

        [HttpGet]
        [Route("Login")]
        public IHttpActionResult Login(string emailaddress, string password)
        {

            var Login = db.Customers.Where(x => (x.EmailAddress == emailaddress) && (x.Password == password)).Select(y => new { y.CustomerId, y.FullName }).SingleOrDefault();
            if (Login != null)
            {
                return Ok(Login);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
