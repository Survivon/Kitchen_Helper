using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kitchen_Helper.Models;

namespace Kitchen_Helper.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        KHDBContext K = new KHDBContext();
        public ActionResult Index()
        {
            ViewBag.user = K.Users.Find(u=>u.id==1).name.ToString();
            return View();
        }

    }
}
