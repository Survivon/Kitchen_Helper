using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KH.Models;

namespace KHMobile.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        KHContext K = new KHContext();
        StorageController S = new StorageController();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email,string password) 
        {
            User U = K.Users.Where(u => u.email.Contains(email)).FirstOrDefault();
            if (U == null)
            {

                ViewBag.Reg = "You don't have an account";
                return View("../Home/Index");
            }
            if (U.password.Contains(password))
            {
                Session["id"] = U.id;
                return S.LoadStorage(U.id);
            }
            else 
            {
                ViewBag.Reg = "Login or password isn't correct";
                return View("../Home/Index");
            }
             
        }
    }
}
