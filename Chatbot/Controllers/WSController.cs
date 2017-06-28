using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApiAiSDK;
using ApiAiSDK.Model;
using Chatbot.Filters;
using System.Web.Helpers;

namespace Chatbot.Controllers
{
    public class WSController : Controller
    {

        ChatbotContainer db = new ChatbotContainer();

        // GET: WS
        public ActionResult Index()
        {
            return View();
        }

        //[AllowCrossSiteJson]
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (!(username == null || username == "" || password == null || password == ""))
            {
                password = Crypto.SHA256(password);

                // Check credentials
                User user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    return Json(new
                    {
                        status = 200
                    });
                }
                // Invalid credentials
                else
                {
                    return Json(new
                    {
                        status = 400,
                        message = "Invalid username or password"
                    });
                }

            }

            return Json(new
            {
                status = 400,
                message = "All fields are mandatory"
            });
        }

        //[AllowCrossSiteJson]
        [HttpPost]
        public ActionResult Signup(User user)
        {
            if (user.Username == null || user.Password == null || user.Name == null || user.Email == null || user.Age == null)
            {
                return Json(new
                {
                    status = 400,
                    message = "All fields are mandatory"
                });
            }

            // Check credentials
            User existingUser = db.Users.FirstOrDefault(u => u.Username == user.Username);

            if (existingUser == null)
            {
                user.Password = Crypto.SHA256(user.Password);

                db.Users.Add(user);

                db.SaveChanges();

                return Json(new
                {
                    status = 200
                });
            }
            // Invalid credentials
            else
            {
                return Json(new
                {
                    status = 400,
                    message = "Username already exists"
                });
            }
        }

        //[AllowCrossSiteJson]
        [HttpPost]
        public ActionResult SendTextMessage(String text, List<AIContext> contexts)
        {
            if (HomeController.apiAi == null)
            {
                var config = new AIConfiguration("2fe096a4e267427c9d3443810a7b82e2", SupportedLanguage.English);
                HomeController.apiAi = new ApiAi(config);
            }

            RequestExtras re = new RequestExtras();

            // TODO: Get the context
            //re.Contexts = (List<AIContext>)HttpContext.Session["Contexts"];
            re.Contexts = contexts;

            AIResponse response = HomeController.apiAi.TextRequest(text, re);

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

                return Json(new
                {
                    Response = response.Result.Fulfillment.Speech,
                    contexts = contexts
                });
            }
        } 
    }
}