using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KH.Models;

namespace KHMobile.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/
        KHContext K = new KHContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MenuList() 
        {
            string listmenu = K.Users.Find(Convert.ToInt32(Session["id"])).menulist;
            List<int> idmenu = convert(listmenu);
            List<string> listnamemenu = new List<string>();
            foreach (var i in idmenu)
            {
                listnamemenu.Add(K.Menus.Where(m => m.id == i).FirstOrDefault().name);
            }
            ViewBag.listmenu = idmenu;
            ViewBag.listnamemenu = listnamemenu;
            return View("../Menu/MenuList");
        }

        private List<int> convert(string s)
        {
            List<int> listmenu = new List<int>();
            s += '#';
            int count = 0;
            while (s[count] != '#')
            {
                int i = count;
                string idfriends = "";
                while (s[i] != ';')
                {
                    idfriends += s[i];
                    i++;
                }
                listmenu.Add(Convert.ToInt32(idfriends));
                count = i + 1;
            }
            return listmenu;
        }

        [HttpPost]
        public ActionResult Menu(int idmenu) 
        {
            Menu M = K.Menus.Find(idmenu);
            ViewBag.name = M.name;
            ViewBag.desc = M.description;
            List<int> recipeid = convert(M.listrecipe);
            List<Recipe> recipe = new List<Recipe>();
            foreach (var i in recipeid) 
            {
                recipe.Add(K.Recipes.Find(i));
            }
            ViewBag.recipe = recipe;
            return View("../Menu/Menu");
        }
    }
}
