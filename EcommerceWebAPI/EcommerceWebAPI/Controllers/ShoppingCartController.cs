using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EcommerceWebAPI.Models;


namespace EcommerceWebAPI.Controllers
{
    [RoutePrefix("api/ShoppingCart")]
    public class ShoppingCartController : ApiController
    {
        EcommerceDBContext db = new EcommerceDBContext();
        

        //Get
        [HttpGet]
        //[Route("ShoppingCartList")]
        public IEnumerable<ShoppingCart> Get()
        {
            return db.ShoppingCarts.ToList();
        }

        [HttpGet]
        [Route("GetCartDetailsbyCustomer")]
        public IHttpActionResult GetId(int customerid)
        {
            var res = db.Database.SqlQuery<GetCartDetailsbyCustomer>(
                "EXEC GetCartDetailsbyCustomer @cid",
                new SqlParameter("@cid", customerid)).ToList();

            if (res != null)
            {
                return Ok(res);
            }
            return NotFound();

        }
      

        //Get by ID
        [HttpGet]
        //[Route("ShoppingCartListByID")]
        public ShoppingCart Get(int id)
        {
            return db.ShoppingCarts.FirstOrDefault(x => x.CartId == id);

        }



        //post or add
        [HttpPost]
        //[Route("AddShoppingCart")]
        public IHttpActionResult PostShoppingcart([FromBody] ShoppingCart sp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("validation Failed");
            }
            db.ShoppingCarts.Add(new ShoppingCart()
            {
                CartId = sp.CartId,
                CustomerID = sp.CustomerID,
                Quantity = sp.Quantity,
                ProductId = sp.ProductId,
                DateCreated = sp.DateCreated,
            });

            db.SaveChanges();
            return Ok("Success"); // we can add instead od success any method name.
        }

        //put or edit
        [HttpPut]
        //[Route("EditShoppingCartByID")]
        public IHttpActionResult Put(int Id, [FromBody] ShoppingCart sc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid ModelState");
            }
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(Id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            // Update existingCustomer properties with values from updatedCustomer
            shoppingCart.CustomerID = sc.CustomerID;
            shoppingCart.Quantity = sc.Quantity;
            shoppingCart.ProductId = sc.ProductId;
            shoppingCart.DateCreated = sc.DateCreated;

            db.SaveChanges();
            return Ok("Updated");
        }

        //delete
        [HttpDelete]
        //[Route("DeleteShoppingCartByID")]
        public IHttpActionResult Delete(int id)
        {
            ShoppingCart shoppingcart = db.ShoppingCarts.Find(id);

            if (shoppingcart == null)
            {
                return NotFound();
            }

            db.ShoppingCarts.Remove(shoppingcart);
            db.SaveChanges();
            return Ok("Deleted");
        }
    }
}
