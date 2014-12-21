using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KH.Models;
using System.Data;

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
        public ActionResult Registration(string name,string surname,string email,string password) 
        {
            User U = new User { name=name,surname=surname,email=email,password=password,role=1};
           
            K.Users.Add(U);
            K.SaveChanges();

            Session["id"] = K.Users.Where(u => u.name == name).FirstOrDefault().id;
            U.storage = K.Users.Where(u => u.name == name).FirstOrDefault().id;
            K.Entry(U).State = EntityState.Modified;
            K.SaveChanges();
            return View("../Main/Main");
        }
    }
}
