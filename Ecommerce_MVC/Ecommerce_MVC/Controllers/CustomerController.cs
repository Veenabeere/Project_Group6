using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ecommerce_MVC.Models;
namespace Ecommerce_MVC.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
       [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(customer customer)
        {
            // Handle form submission and call the Web API to add the customer
            // You can use HttpClient to interact with your Web API
            // For simplicity, you can add validation and error handling here
            using (HttpClient client = new HttpClient())
            {
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }
                client.BaseAddress = new Uri("https://localhost:44343/api");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("api/Customer", customer).Result;
               
                if (response.IsSuccessStatusCode)
                {
                    // Registration successful, you can redirect to a success page or login
                    ViewBag.result = "Registration successfull";
                    return RedirectToAction("Login");
                }
                else
                {
                    // Registration failed, handle errors
                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                    return View(customer);
                }
            }
        }

       

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(customer loginCustomer)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44343/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Build the login URL with parameters
                string loginUrl = $"Customer/Login?emailaddress={loginCustomer.EmailAddress}&password={loginCustomer.Password}";
                HttpResponseMessage response = client.GetAsync(loginUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    var responseData = response.Content.ReadAsAsync<dynamic>().Result;
                    // Assuming the response contains CustomerId and FullName
                    int customerId = responseData.CustomerId;
                    string fullName = responseData.FullName;
                    // You can use these values as needed, for example, set them in session
                    Session["CustomerId"] = customerId;
                    Session["FullName"] = fullName;
                    // Login successful
                    return RedirectToAction("Index", "Home"); // Redirect to your dashboard or another secure area
                }
                else
                {
                    // Login failed, handle errors
                    ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials and try again.");
                    return View();
                }
            }
        }

        public ActionResult Signout()
        {
            Session["CustomerId"] = null;
            //FormsAuthentication.SignOut();
            //return RedirectToAction("Login");
            return View();
        }




    }
}