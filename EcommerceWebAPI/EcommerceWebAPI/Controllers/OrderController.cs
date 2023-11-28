using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EcommerceWebAPI.Models;

namespace EcommerceWebAPI.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        EcommerceDBContext db = new EcommerceDBContext();

        //Get
        [HttpGet]
        //[Route("OrderList")]
        public IEnumerable<Order> Get()
        {
            return db.Orders.ToList();
        }
        // GET api/orders/{customerId}

        [HttpGet]
        [Route("GetOrderbyCustomers")]
        public IHttpActionResult GetOrdersByCustomerId(int customerId)
        {
            try
            {
                var orders = db.Orders.Where(o => o.CustomerId == customerId).ToList();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }






        //Get by ID
        [HttpGet]
        [Route("OrderListByID")]
        public Order Get(int id)
        {
            return db.Orders.FirstOrDefault(x => x.OrderId == id);

        }

        //post or add
        [HttpPost]
        //[Route("AddOrder")]
        public IHttpActionResult PostOrder([FromBody] Order o)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("validation Failed");
            }
            db.Orders.Add(new Order()
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                ShipDate = o.ShipDate,
                ProductId=o.ProductId,
            });

            db.SaveChanges();
            return Ok("Success"); // we can add instead od success any method name.
        }

        //put or edit
        [HttpPut]
        [Route("EditOrderByID")]
        public IHttpActionResult Put(int Id, [FromBody] Order o)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid ModelState");
            }
            Order order = db.Orders.Find(Id);
            if (order == null)
            {
                return NotFound();
            }
            // Update existingCustomer properties with values from updatedCustomer
            order.CustomerId = o.CustomerId;
            order.OrderDate = o.OrderDate;
            order.ShipDate = o.ShipDate;
            order.ProductId = o.ProductId;
           
            db.SaveChanges();
            return Ok("Updated");
        }

        //delete
        [HttpDelete]
        [Route("DeleteOrder")]
        public IHttpActionResult Delete(int id)
        {
            Order order = db.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();
            return Ok("Deleted");
        }

    }
}
