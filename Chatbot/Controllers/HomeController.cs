using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chatbot.Models;

namespace Chatbot.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // Show login page if user is not loggedin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Do login for a user and redirect to specific page w.r.t. user role
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // LoyaltyContainer dataContext = new LoyaltyContainer();
                // Check credentials
                //User user = dataContext.Users.FirstOrDefault(u => u.Username == loginModel.Username && u.Password == loginModel.Password && u.Status == true);

                User user = new User();

                if (user != null)
                {
                    HttpContext.Session["LoggedInUser"] = user;

                    return RedirectToAction("Index", "Home");
                }
                // Invalid credentials
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            return View();
        }
    }
}