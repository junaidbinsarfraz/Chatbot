using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Chatbot.Models;
using ApiAiSDK;
using ApiAiSDK.Model;
using System.Web.Helpers;

namespace Chatbot.Controllers
{
    public class HomeController : Controller
    {

        ChatbotContainer db = new ChatbotContainer();
        public static ApiAi apiAi;
        private List<AIContext> contexts;

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
                string password = Crypto.SHA256(loginModel.Password);

                // Check credentials
                User user = db.Users.FirstOrDefault(u => u.Username == loginModel.Username && u.Password == password);

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

        // Show signup page if user is not loggedin
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        // Do signup for a user and redirect to specific page w.r.t. user role
        [HttpPost]
        public ActionResult Signup(User user)
        {
            if (ModelState.IsValid)
            {
                // Check credentials
                User existingUser = db.Users.FirstOrDefault(u => u.Username == user.Username);

                if (existingUser == null)
                {
                    user.Password = Crypto.SHA256(user.Password);

                    db.Users.Add(user);

                    db.SaveChanges();

                    HttpContext.Session["LoggedInUser"] = db.Users.FirstOrDefault(u => u.Username == user.Username);

                    return RedirectToAction("Index", "Home");
                }
                // Invalid credentials
                else
                {
                    ModelState.AddModelError("", "Username already exists");
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult SendTextMessage(String text)
        {
            if (apiAi == null)
            {
                var config = new AIConfiguration("2fe096a4e267427c9d3443810a7b82e2", SupportedLanguage.English);
                apiAi = new ApiAi(config);
            }

            RequestExtras re = new RequestExtras();

            re.Contexts = (List<AIContext>) HttpContext.Session["Contexts"];
            
            AIResponse response = apiAi.TextRequest(text, re);

            if (response.IsError)
            {
                Console.Out.Write("There is an error");

                return Json(new
                {
                    Response = "error"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Console.Out.Write(response.Result.Fulfillment.Speech);

                if (response.Result.Contexts != null)
                {

                    contexts = new List<AIContext>();

                    foreach (var aoc in response.Result.Contexts.ToList())
                    {
                        AIContext ac = new AIContext();

                        ac.Lifespan = aoc.Lifespan;
                        ac.Name = aoc.Name;
                        ac.Parameters = aoc.Parameters.ToDictionary(k => k.Key, k => k.Value.ToString()); ;

                        contexts.Add(ac);
                    }

                }
                else
                {
                    contexts = null;
                }

                HttpContext.Session["Contexts"] = contexts;

                return Json(new
                {
                    Response = response.Result.Fulfillment.Speech
                }, JsonRequestBehavior.AllowGet);
            }

            
        } 
    }
}