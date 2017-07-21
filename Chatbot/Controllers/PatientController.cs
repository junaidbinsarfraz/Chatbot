﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chatbot.App_Start;
using System.Net;

namespace Chatbot.Controllers
{
    public class PatientController : Controller
    {
        ChatbotContainer db = new ChatbotContainer();

        [HttpGet]
        public ActionResult Index()
        {
            User loggedInUser = (User)HttpContext.Session["LoggedInUser"];

            if (!RoleManager.isPatient(loggedInUser))
            {
                return View("Login", "Home");
            }

            return View(loggedInUser as User);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            User loggedInUser = (User)HttpContext.Session["LoggedInUser"];

            if (!RoleManager.isPatient(loggedInUser))
            {
                return View("Login", "Home");
            }

            return View(loggedInUser as User);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {

            User loggedInUser = (User)HttpContext.Session["LoggedInUser"];

            if (!RoleManager.isPatient(loggedInUser))
            {
                return View("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                User oldUser = db.Users.FirstOrDefault(u => u.Id == user.Id);

                // TODO: save edit patient

                //oldUser.FullName = user.FullName;
                //oldUser.UserName = user.UserName;
                //oldUser.NRIC = user.NRIC;
                //oldUser.Age = user.Age;
                //oldUser.ContactNo = user.ContactNo;
                //oldUser.Patient.Disease = user.Patient.Disease;
                //oldUser.Email = user.Email;
                //oldUser.Patient.Occupation = user.Patient.Occupation;
                //oldUser.Gender = user.Gender;
                //oldUser.Address = user.Address;
                //oldUser.Comments = user.Comments;

                db.SaveChanges();

                HttpContext.Session["LoggedInUser"] = oldUser;

                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Detail(long? id)
        {
            User loggedInUser = (User)HttpContext.Session["LoggedInUser"];

            if (!RoleManager.isLoggedIn(loggedInUser))
            {
                return View("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }
    }
}