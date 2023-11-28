using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EcommerceWebAPI.Models;

namespace EcommerceWebAPI.Controllers
{
    [RoutePrefix("api/OrderDetails")]
    public class OrderDetailController : ApiController
    {
        EcommerceDBContext db = new EcommerceDBContext();

        //Get
        [HttpGet]
        [Route("OrderDetailsList")]
        public IEnumerable<OrderDetail> Get()
        {
            return db.OrderDetails.ToList();
        }

        //Get by ID
        [HttpGet]
        [Route("OrderListByID")]
        public OrderDetail Get(int id)
        {
            return db.OrderDetails.FirstOrDefault(x => x.OrderDetailsID == id);

        }

        //post or add
        [HttpPost]
        [Route("AddOrderDetails")]
        public IHttpActionResult PostOrderDetails([FromBody] OrderDetail od)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("validation Failed");
            }
            db.OrderDetails.Add(new OrderDetail()
            {
                OrderDetailsID = od.OrderDetailsID,
                OrderId = od.OrderId,
                CustomerId = od.CustomerId,
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                UnitCost = od.UnitCost,
            });

            db.SaveChanges();
            return Ok("Success"); // we can add instead od success any method name.
        }

        //put or edit
        [HttpPut]
        [Route("EditOrderDetailsByID")]
        public IHttpActionResult Put(int Id, [FromBody] OrderDetail od)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid ModelState");
            }
            OrderDetail orderdetails = db.OrderDetails.Find(Id);
            if (orderdetails == null)
            {
                return NotFound();
            }
            // Update existingCustomer properties with values from updatedCustomer
            orderdetails.OrderId = od.OrderId;
            orderdetails.CustomerId = od.CustomerId;
            orderdetails.ProductId = od.ProductId;
            orderdetails.Quantity = od.Quantity;
            orderdetails.UnitCost = od.UnitCost;
            db.SaveChanges();
            return Ok("Updated");
        }

        //delete
        [HttpDelete]
        [Route("DeleteOrderDetailsByID")]
        public IHttpActionResult Delete(int id)
        {
            OrderDetail orderdetail = db.OrderDetails.Find(id);

            if (orderdetail == null)
            {
                return NotFound();
            }

            db.OrderDetails.Remove(orderdetail);
            db.SaveChanges();
            return Ok("Deleted");
        }

    }
}
