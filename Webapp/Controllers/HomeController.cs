using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webapp.BusinessLayer;
using Webapp.Models;
using Webapp.ViewModel;

namespace Webapp.Controllers
{
    public class HomeController : Controller
    {
        public readonly IAuthentication _IAuthentication;
        public HomeController(IAuthentication authentication)
        {
            _IAuthentication = authentication;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
         var userdetails=   _IAuthentication.GetUserDetails(model.UserName, model.Password);
            if (userdetails != null)
            {
                if (userdetails.UserName != "")
                {
                    Session["UserName"] = model.UserName;
                    return View("Index");
                }
            }
            else
            {
                ViewBag.NotValidUser = "Incorrect user name or password";

            }


            return View("Login");
        }


    }
}