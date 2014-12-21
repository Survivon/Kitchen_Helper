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
        public ActionResult MainLoad() 
        {
            User U = K.Users.Find(Convert.ToInt32(Session["id"]));            
            List<int> listmenu = menulist(U.menulist);
            List<string> listnamemenu = new List<string>();
            foreach (var i in listmenu) 
            {
                listnamemenu.Add(K.Menu.Where(m => m.id == i).FirstOrDefault().name);
            }

            int idstorage = U.storage;
            Storage ProductinStorage = K.Storage.Where(s => s.id == idstorage).FirstOrDefault();
            List<string> nameproduct = stringconvector(ProductinStorage.nameproduct);
            List<int> countproduct = menulist(ProductinStorage.count);
            List<string> countname = stringconvector(ProductinStorage.countname);

            return View();
        }

        private List<string> stringconvector(string list) 
        {
            List<string> liststring = new List<string>();
            list += '#';
            int i = 0;
            string localstr = "";
            while (list[i] != '#')
            {
                localstr += list[i];
                if (list[i] == ';')
                {
                    i++;
                    liststring.Add(localstr);
                }
                i++;
            }
            return liststring;
        }

        private List<int> menulist(string list) 
        {
            List<int> listmenu = new List<int>();
            list += '#';
            int i =0;
            string localstr = "";
            while (list[i] != '#') 
            {
                localstr += list[i];
                if (list[i] == ';') 
                {
                    i++;
                    listmenu.Add(Convert.ToInt32(localstr));
                }
                i++;
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
            return MainLoad();
        }

        [HttpPost]
        public ActionResult CreateNewMenu(string name,string description) 
        {
            Menu M = new Menu { name=name,
            description=description};
            K.Menu.Add(M);
            K.SaveChanges();
            int id = K.Menu.Where(m => m.name == name).FirstOrDefault().id;
            User U = K.Users.Find(Convert.ToInt32(Session["id"]));
            U.menulist += Convert.ToString(id) + ';';
            K.Entry(U).State = EntityState.Modified;
            K.SaveChanges();
            return MainLoad();
        }

    }
}
