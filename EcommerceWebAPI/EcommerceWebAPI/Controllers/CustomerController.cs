using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EcommerceWebAPI.Models;

namespace EcommerceWebAPI.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        EcommerceDBContext db = new EcommerceDBContext();

        ////Get
        //[HttpGet]
        //[Route("CustomerList")]
        public IEnumerable<Customer> Get()
        {
            return db.Customers.ToList();
        }

        //Get by ID
        //[HttpGet]
        //[Route("CustomerListByID")]
        public Customer Get(int id)
        {
            return db.Customers.FirstOrDefault(x => x.CustomerId == id);
        }

        //post or add
        //[HttpPost]
        //[Route("AddCustomer")]
        public IHttpActionResult PostCustomer([FromBody] Customer c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("validation Failed");
            }
            db.Customers.Add(new Customer()
            {
                CustomerId = c.CustomerId,
                FullName = c.FullName,
                EmailAddress = c.EmailAddress,
                Password = c.Password,
                DeliveryAddress = c.DeliveryAddress,
                PhoneNumber = c.PhoneNumber
            });

            db.SaveChanges();
            return Ok("Success"); // we can add instead od success any method name.
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

        
        public IHttpActionResult Put(int Id, [FromBody] Customer c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid ModelState");
            }
            Customer customer = db.Customers.Find(Id);
            if (customer == null)
            {
                return NotFound();
            }
            // Update existingCustomer properties with values from updatedCustomer
            customer.FullName = c.FullName;
            customer.EmailAddress = customer.EmailAddress;
            customer.Password = c.Password;
            customer.DeliveryAddress = c.DeliveryAddress;
            customer.PhoneNumber = c.PhoneNumber;
            db.SaveChanges();
            return Ok("Updated");
        }

        //delete
        //[HttpDelete]
        //[Route("DeleteCustomerByID")]
        public IHttpActionResult Delete(int id)
        {
            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();

            }

            db.Customers.Remove(customer);
            db.SaveChanges();
            return Ok("Deleted");
        }


    }
}
