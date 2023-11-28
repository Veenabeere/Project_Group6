using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ecommerce_MVC.Models;
using Newtonsoft.Json;

namespace Ecommerce_MVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        //category list
        public ActionResult Category()
        {
            IEnumerable<Category> categorylist = null;
            using (var webclient = new HttpClient())
            {
                webclient.BaseAddress = new Uri("https://localhost:44343/api/");
                var responsetask = webclient.GetAsync("Category");
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultdata = result.Content.ReadAsStringAsync().Result;
                    categorylist = JsonConvert.DeserializeObject<List<Category>>(resultdata);
                }
                else
                {
                    categorylist = Enumerable.Empty<Category>();
                    ModelState.AddModelError(string.Empty, "Some Error Occured..Try Later");
                }
                return View(categorylist);
            }
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            using (HttpClient client = new HttpClient())
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }
                client.BaseAddress = new Uri("https://localhost:44343/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("api/Category", category).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Registration successful, you can redirect to a success page or login
                    ViewBag.result = "Category added successfull";
                    return RedirectToAction("~/Customer/Login");
                }
                else
                {
                    // Registration failed, handle errors
                    ModelState.AddModelError(string.Empty, "Not able to add category. Please try again.");
                    return View(category);
                }
            }
        }

        [HttpGet]
        public ActionResult Product(int categoryId)
        {
            IEnumerable<Product> product = null;
            using (var webclient = new HttpClient())
            {
                webclient.BaseAddress = new Uri("https://localhost:44343/api/");
                var responsetask = webclient.GetAsync("Product/GetProductsByCategory?categoryId=" + categoryId);
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultdata = result.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<List<Product>>(resultdata);
                }
                else
                {
                    product = Enumerable.Empty<Product>();
                    ModelState.AddModelError(string.Empty, "Some Error Occured..Try Later");
                }
                return View(product);
            }

        }





        [HttpGet]
        public ActionResult Addtocart()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Addtocart1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Addtocart1(ShoppingCart sc)
        {
            using (HttpClient client = new HttpClient())
            {
                if (!ModelState.IsValid)
                {
                    return View(sc);
                }
                client.BaseAddress = new Uri("https://localhost:44343/api");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("api/ShoppingCart", sc).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Registration successful, you can redirect to a success page or login
                    ViewBag.result = "Registration successfull";
                    // return RedirectToAction("Login", "Customer");
                    TempData["successmessage"] = "Item added to cart successfully";
                    return RedirectToAction("Category", "Category");


                }
                else
                {
                    // Registration failed, handle errors
                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                    return View(sc);
                }
            }
        }







        //[HttpPost]
        //public async Task<ActionResult> AddToShoppingCart(int customerId, int productId, int quantity)
        //{
        //    using (HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:44343/api/") })
        //    {
        //        ShoppingCart shoppingCart = new ShoppingCart
        //        {
        //            CustomerID = customerId,
        //            ProductId = productId,
        //            Quantity = quantity,
        //            DateCreated = DateTime.Now
        //        };
        //        HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/ShoppingCart/AddShoppingCart", shoppingCart);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index", "Home"); // Redirect to your desired page on success
        //        }
        //        else
        //        {
        //            // Handle error, maybe return a view with an error message
        //            return View("Error");
        //        }
        //    }
        //}

        public ActionResult GetGetCartDetails1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCartDetails1( int Quantity, int ProductId)
        {
            ShoppingCart sc = new ShoppingCart();
            sc.CustomerID = Convert.ToInt32(Session["customerId"]);
            sc.Quantity = Quantity;
            sc.ProductId = ProductId;
            sc.DateCreated = DateTime.Now;

            using (HttpClient client = new HttpClient())
            {
                if (!ModelState.IsValid)
                {
                    RedirectToAction("Category", "Category");
                }
                client.BaseAddress = new Uri("https://localhost:44343/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("api/ShoppingCart", sc).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Registration successful, you can redirect to a success page or login
                    ViewBag.result = "added successfull";
                    TempData["successmessage"] = "Item added to cart successfully";
                    //return RedirectToAction("GetCartDetails", "Category");

                    return ViewBag("successfull");
                }
                else
                {
                    // Registration failed, handle errors
                    ModelState.AddModelError(string.Empty, "Not able to add category. Please try again.");
                    //return RedirectToAction("Login" ,"Customer");
                    return ViewBag("successfull");

                }
            }
        }


        [HttpGet]
        public ActionResult GetCartDetails(int customerId)
        {
            IEnumerable<GetCartDetailsbyCustomer> getcart = null;
            using (var webclient = new HttpClient())
            {
                webclient.BaseAddress = new Uri("https://localhost:44343/api/");
                var responsetask = webclient.GetAsync("ShoppingCart/GetCartDetailsbyCustomer?customerId=" + customerId);
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultdata = result.Content.ReadAsStringAsync().Result;
                    getcart = JsonConvert.DeserializeObject<List<GetCartDetailsbyCustomer>>(resultdata);
                }
                else
                {
                    getcart = Enumerable.Empty<GetCartDetailsbyCustomer>();
                    ModelState.AddModelError(string.Empty, "Some Error Occured..Try Later");
                }
                return View(getcart);
            }

        }

        [HttpGet]
        public ActionResult Orders(int customerId)
        {
            IEnumerable<Orders> orderlist = null;
            using (var webclient = new HttpClient())
            {
                webclient.BaseAddress = new Uri("https://localhost:44343/api/");
                var responsetask = webclient.GetAsync("Order/GetOrderbyCustomers?customerId="+customerId);
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultdata = result.Content.ReadAsStringAsync().Result;
                    orderlist = JsonConvert.DeserializeObject<List<Orders>>(resultdata);
                }
                else
                {
                    orderlist = Enumerable.Empty<Orders>();
                    ModelState.AddModelError(string.Empty, "Some Error Occured..Try Later");
                }
                return View(orderlist);
            }
        }

        [HttpGet]
        public ActionResult CreateOrders()
        {
            return View();

        }

        [HttpPost]
        public ActionResult CreateOrders(Orders o)
        {
            using (HttpClient client = new HttpClient())
            {
                if (!ModelState.IsValid)
                {
                    return View(0);
                }
                client.BaseAddress = new Uri("https://localhost:44343/api");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("api/Order", o).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Registration successful, you can redirect to a success page or login
                    ViewBag.result = "Order successfull";
                    // return RedirectToAction("Login", "Customer");
                    TempData["successmessage"] = "Item added to cart successfully";
                    return RedirectToAction("Category", "Category");


                }
                else
                {
                    // Registration failed, handle errors
                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                    return View(o);
                }
            }
        }





















    }
}