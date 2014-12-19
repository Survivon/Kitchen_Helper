using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KH.Models;

namespace KH.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        KHContext K = new KHContext();
        public ActionResult Index()
        {           
            return View();
        }

    }
}
