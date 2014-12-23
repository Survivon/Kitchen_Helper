using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KH.Models;
using System.Data;

namespace KH.Controllers
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
        public ActionResult MenuEditor(int idmenu) 
        {
            Menu ThisMenu = K.Menus.Where(m => m.id == idmenu).FirstOrDefault();
            string recipe = ThisMenu.listrecipe;
            List<int> recipelist = menulist(recipe);
            List<string> namerecipe = new List<string>();
            foreach (var i in recipelist) 
            {
                namerecipe.Add(K.Recipes.Find(i).name);
            }
            ViewBag.namerecipe = namerecipe;
            ViewBag.recipeid = recipelist;
            ViewBag.namemenu = ThisMenu.name;
            ViewBag.thismenuid = ThisMenu.id;
            return View("../Menus/Menu");
        }

        private List<int> menulist(string menu) 
        {
            List<int> listmenu = new List<int>();
            menu += '#';
            int count = 0;
            while (menu[count] != '#')
            {
                int i = count;
                string idfriends = "";
                while (menu[i] != ';')
                {
                    idfriends += menu[i];
                    i++;
                }
                listmenu.Add(Convert.ToInt32(idfriends));
                count = i + 1;
            }
            return listmenu;
        }

        [HttpPost]
        public ActionResult DelRecipefromMenu(int idmenu,int idrecipe) 
        {
            Menu M = K.Menus.Find(idmenu);
            M.listrecipe = recipeworker(M.listrecipe, idrecipe,false);
            try
            {
                K.Entry(M).State = EntityState.Modified;
                K.SaveChanges();
                ViewBag.delresult = "Recipe - "+K.Recipes.Find(idrecipe).name+" was delete from this menu";
            }
            catch (Exception e) 
            {
                ViewBag.delresult = "Recipe - " + K.Recipes.Find(idrecipe).name + " wasn't delete from this menu";
            }
            return MenuEditor(idmenu);
        }

        private string recipeworker(string listrecipe,int delrecipe,bool mode) 
        {
            List<int> recipelist = menulist(listrecipe);
            if (mode)
                recipelist.Add(delrecipe);
            else
                recipelist.Remove(delrecipe);
            string newlistrecipe = "";
            foreach (var i in recipelist) 
            {
                newlistrecipe += i;
                newlistrecipe += ';';
            }
            return newlistrecipe;
        }

        [HttpPost]
        public ActionResult AddNewRecipe(int idmenu,int idrecipe) 
        {
            Menu M = K.Menus.Find(idmenu);
            if (check(M.listrecipe, idrecipe)) 
            {
                ViewBag.donerecipe = "This Recipe - "+K.Recipes.Find(idrecipe).name+" contains in menu - "+M.name;
                return MenuEditor(idmenu);
            }
            else
                
            M.listrecipe = recipeworker(M.listrecipe, idrecipe, true);
            K.Entry(M).State = EntityState.Modified;
            K.SaveChanges();
            return MenuEditor(idmenu);
        }

        [HttpPost]
        public ActionResult AddnewMenu(string name,string description) 
        {
            Menu M = new Menu { name=name,
            description=description,listrecipe=""};
            K.Menus.Add(M);
            K.SaveChanges();
            User U = K.Users.Find(Convert.ToInt32(Session["id"]));
            int idmenu = K.Menus.Where(m => m.name.Contains(name)).FirstOrDefault().id;
            U.menulist = recipeworker(U.menulist, idmenu, true);
            K.Entry(U).State = EntityState.Modified;
            K.SaveChanges();
            return MenuEditor(idmenu);
        }

        private bool check(string listrecipe,int idrecipe) 
        {
            return listrecipe.Contains(Convert.ToString(idrecipe));
        }

        
    }
}
