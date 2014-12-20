using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KH.Models;
using System.Security.Cryptography;

namespace KH.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        KHContext K = new KHContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email,string password) 
        {
            User U = K.User.Where(u=>u.email==email).FirstOrDefault();
            if (U == null) 
            {
                return View();
            }
            if(password==U.password)
            {
                Session["id"] = U.id;
                return View();
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Registration() 
        {
            return View();
        }

    }
}
