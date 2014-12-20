using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KH.Models;

namespace KH.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/
        KHContext K = new KHContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User U) 
        {
            U.role = 1;
            K.User.Add(U);
            K.SaveChanges();
            Session["id"] = K.User.Find(U).id;
            return View();
        }
    }
}
