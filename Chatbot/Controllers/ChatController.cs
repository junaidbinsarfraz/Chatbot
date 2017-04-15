using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chatbot.Models;
using ApiAiSDK;

namespace Chatbot.Controllers
{
    public class ChatController : Controller
    {

        ChatbotContainer db = new ChatbotContainer();

        private ApiAi apiAi;

        public ActionResult Index()
        {
            return View();
        }
        
    }
}