using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EcommerceWebAPI.Models;

namespace EcommerceWebAPI.Controllers
{
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        EcommerceDBContext db = new EcommerceDBContext();

        //Get
        [HttpGet]
        //[Route("CategoryList")]
        public IEnumerable<Category> Get()
        {
            return db.Categories.ToList();
        }

        //Get by ID
        [HttpGet]
        //[Route("CategoryListByID")]
        public Category Get(int id)
        {
            return db.Categories.FirstOrDefault(x => x.CategoryId == id);
        }

        //post or add
        [HttpPost]
        //[Route("AddCategory")]
        public IHttpActionResult PostCategory([FromBody] Category cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("validation Failed");
            }
            db.Categories.Add(new Category()
            {
                CategoryId = cat.CategoryId,
                CategoryName = cat.CategoryName,
                Description = cat.Description,
                CategoryImage = cat.CategoryImage,

            });
            db.SaveChanges();
            return Ok("Success");
        }

        //put or edit
        [HttpPut]
        //[Route("EditCategoryByID")]
        public IHttpActionResult Put(int Id, [FromBody] Category cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid ModelState");
            }
            Category category = db.Categories.Find(Id);
            if (category == null)
            {
                return NotFound();
            }
            // Update existingCustomer properties with values from updatedCustomer
            category.CategoryName = cat.CategoryName;
            category.Description = cat.Description;
            category.CategoryImage = cat.CategoryImage;
            db.SaveChanges();
            return Ok("Updated");
        }

        [HttpDelete]
        [Route("DeleteCategoryByID")]
        public IHttpActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();

            }

            db.Categories.Remove(category);
            db.SaveChanges();
            return Ok("Deleted");
        }
        //public class CategoryResponse
        //{
        //    public int CategoryId { get; set; }
        //    public string CategoryName { get; set; }
        //    public string Description { get; set; }
        //    public string CategoryImage { get; set; }
        //}
        //public IEnumerable<CategoryResponse> Get()
        //{
        //    var categories = db.Categories.ToList();
        //    var categoryResponses = categories.Select(category => new CategoryResponse
        //    {
        //        CategoryId = category.CategoryId,
        //        CategoryName = category.CategoryName,
        //        Description = category.Description,
        //        CategoryImage = Convert.ToBase64String(category.CategoryImage)
        //    });
        //    return categoryResponses;
        //}
        //public IHttpActionResult Get(int id)
        //{
        //    var category = db.Categories.FirstOrDefault(x => x.CategoryId == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    var categoryResponse = new CategoryResponse
        //    {
        //        CategoryId = category.CategoryId,
        //        CategoryName = category.CategoryName,
        //        Description = category.Description,
        //        CategoryImage = Convert.ToBase64String(category.CategoryImage)
        //    };
        //    return Ok(categoryResponse);
        //}

        //public class CategoryRequest
        //{
        //    public int CategoryId { get; set; }
        //    public string CategoryName { get; set; }
        //    public string Description { get; set; }
        //    public string CategoryImage { get; set; }
        //}

        //// Post or Add
        //[HttpPost]
        //[Route("AddCategory")]
        //public IHttpActionResult PostCategory([FromBody] CategoryRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Validation failed");
        //    }
        //    var newCategory = new Category
        //    {
        //        CategoryName = request.CategoryName,
        //        Description = request.Description,
        //        CategoryImage = Convert.FromBase64String(request.CategoryImage)
        //    };
        //    db.Categories.Add(newCategory);
        //    db.SaveChanges();
        //    // Optionally, you can return the newly created category
        //    var response = new CategoryResponse
        //    {
        //        CategoryId = newCategory.CategoryId,
        //        CategoryName = newCategory.CategoryName,
        //        Description = newCategory.Description,
        //        CategoryImage = Convert.ToBase64String(newCategory.CategoryImage)
        //    };
        //    return Ok(response);
        //}








    }
}
