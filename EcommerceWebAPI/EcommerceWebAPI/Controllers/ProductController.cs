using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EcommerceWebAPI.Models;

namespace EcommerceWebAPI.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        EcommerceDBContext db = new EcommerceDBContext();

        [HttpGet]
        [Route("GetProductsByCategory")]
        public IHttpActionResult GetProductsByCategory(int categoryId)
        {
            try
            {
                var products = db.Products
                    .Where(p => p.CategoryId == categoryId)
                    .Select(p => new
                    {
                        p.ProductId,
                        p.ModelNumber,
                        p.ModelName,
                        p.UnitCost,
                        p.Description,
                        p.ProductImage
                    })
                    .ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }




















        //Get
        [HttpGet]
        [Route("ProductList")]
        public IEnumerable<Product> Get()
        {
            return db.Products.ToList();
        }

        ////Get by ID
        //[HttpGet]
        //[Route("ProductListByID")]
        //public Product Get(int id)
        //{
        //    return db.Products.FirstOrDefault(x => x.ProductId == id);
        //}

        //post or add
        [HttpPost]
        [Route("AddProduct")]
        public IHttpActionResult PostProduct([FromBody] Product p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("validation Failed");
            }
            db.Products.Add(new Product()
            {
                ProductId = p.ProductId,
                CategoryId = p.CategoryId,
                ModelNumber = p.ModelNumber,
                ModelName = p.ModelName,
                UnitCost = p.UnitCost,
                Description = p.Description,
                ProductImage = p.ProductImage,
            });

            db.SaveChanges();
            return Ok("Success"); // we can add instead od success any method name.
        }

        //put or edit
        [HttpPut]
        [Route("EditProductByID")]
        public IHttpActionResult Put(int Id, [FromBody] Product p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid ModelState");
            }
            Product product = db.Products.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            // Update existingCustomer properties with values from updatedCustomer
            product.CategoryId = p.CategoryId;
            product.ModelNumber = p.ModelNumber;
            product.ModelName = p.ModelName;
            product.UnitCost = p.UnitCost;
            product.Description = p.Description;
            product.ProductImage = p.ProductImage;
            db.SaveChanges();
            return Ok("Updated");
        }

        //delete
        [HttpDelete]
        [Route("DeleteProductByID")]
        public IHttpActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();
            return Ok("Deleted");
        }

    }
}
