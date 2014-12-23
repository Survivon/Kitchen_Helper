using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KH.Models;
using System.Data;

namespace KH.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/
        KHContext K = new KHContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MainLoad(int id) 
        {
            User U = new Models.User();
            if (id!=null)
                U = K.Users.Find(id);
            else
                U = K.Users.Find(Convert.ToInt32(Session["id"]));
            List<int> listmenu = menulist(U.menulist);
            List<string> listnamemenu = new List<string>();
            foreach (var i in listmenu) 
            {
                listnamemenu.Add(K.Menus.Where(m => m.id == i).FirstOrDefault().name);
            }
            ViewBag.listmenu = listmenu;
            ViewBag.listnamemenu = listnamemenu;
            int idstorage = U.storage;
            Storage ProductinStorage = K.Storages.Where(s => s.id == idstorage).FirstOrDefault();
            if (ProductinStorage == null) 
            {
                ViewBag.boolload = "true";
                return View("../Main/Main");
            }
            
            string storageproductid = ProductinStorage.nameproduct;
            List<int> idproduct = menulist(storageproductid);
            List<string> nameproduct = new List<string>();
            List<int> countproduct = new List<int>();
            List<string> countname = new List<string>();
            foreach(var i in idproduct)
            {
                nameproduct.Add(K.Products.Find(i).name);
                countproduct.Add(K.Products.Find(i).count);
                countname.Add(K.Products.Find(i).countname);
            }
            ViewBag.nameproduct = nameproduct;
            ViewBag.idproduct = idproduct;
            ViewBag.countproduct = countproduct;
            ViewBag.countname = countname;
            return View("../Main/Main");
        }

        private List<string> stringconvector(string list) 
        {
            List<string> liststring = new List<string>();
            list += '#';
            int count = 0;            
            while (list[count] != '#')
            {
                int i = count;
                string idfriends = "";
                while (list[i] != ';')
                {
                    idfriends += list[i];
                    i++;
                }
                liststring.Add(idfriends);
                count = i + 1;
            }
            return liststring;
        }

        private List<int> menulist(string list) 
        {
            List<int> listmenu = new List<int>();
            list += '#';
            int count = 0;
            while (list[count] != '#')
            {
                int i = count;
                string idfriends = "";
                while (list[i] != ';')
                {
                    idfriends += list[i];
                    i++;
                }
                listmenu.Add(Convert.ToInt32(idfriends));
                count = i + 1;
            }
            return listmenu;
        }

        private string returnmenulist(List<int> list) 
        {
            string listmenu = "";
            foreach (var i in list) 
            {
                listmenu += i + ';';
            }
            return listmenu;
        }
        [HttpPost]
        public ActionResult DelMenu(int idmenu) 
        {
            List<int> listmenu = menulist(K.Users.Find(Convert.ToInt32(Session["id"])).menulist);
            listmenu.Remove(idmenu);
            User U = K.Users.Find(Convert.ToInt32(Session["id"]));
            U.menulist = returnmenulist(listmenu);
            K.Entry(U).State = EntityState.Modified;
            K.SaveChanges();
            return MainLoad(Convert.ToInt32(Session["id"]));
        }

        [HttpPost]
        public ActionResult CreateNewMenu(string name,string description) 
        {
            Menu M = new Menu { name=name,
            description=description};
            K.Menus.Add(M);
            K.SaveChanges();
            int id = K.Menus.Where(m => m.name == name).FirstOrDefault().id;
            User U = K.Users.Find(Convert.ToInt32(Session["id"]));
            U.menulist += Convert.ToString(id) + ';';
            K.Entry(U).State = EntityState.Modified;
            K.SaveChanges();
            return MainLoad(Convert.ToInt32(Session["id"]));
        }
        
    }
}
