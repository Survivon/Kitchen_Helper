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
            Menu ThisMenu = K.Menu.Where(m => m.id == idmenu).FirstOrDefault();
            string recipe = ThisMenu.listrecipe;
            List<int> recipelist = menulist(recipe);
            List<string> namerecipe = new List<string>();
            foreach (var i in recipelist) 
            {
                namerecipe.Add(K.Recipe.Find(i).name);
            }
            ViewBag.namerecipe = namerecipe;
            ViewBag.recipeid = recipelist;
            ViewBag.namemenu = ThisMenu.name;
            ViewBag.thismenuid = ThisMenu.id;
            return View();
        }

        private List<int> menulist(string menu) 
        {
            List<int> listmenu = new List<int>();
            menu += '#';
            int i = 0;
            string localstr = "";
            while (menu[i] != '#')
            {
                localstr += menu[i];
                if (menu[i] == ';')
                {
                    i++;
                    listmenu.Add(Convert.ToInt32(localstr));
                }
                i++;
            }
            return listmenu;
        }

        [HttpPost]
        public ActionResult DelRecipefromMenu(int idmenu,int idrecipe) 
        {
            Menu M = K.Menu.Find(idmenu);
            M.listrecipe = recipeworker(M.listrecipe, idrecipe,false);
            try
            {
                K.Entry(M).State = EntityState.Modified;
                K.SaveChanges();
                ViewBag.delresult = "Recipe - "+K.Recipe.Find(idrecipe).name+" was delete from this menu";
            }
            catch (Exception e) 
            {
                ViewBag.delresult = "Recipe - " + K.Recipe.Find(idrecipe).name + " wasn't delete from this menu";
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
                newlistrecipe += i + ';';
            }
            return newlistrecipe;
        }

        [HttpPost]
        public ActionResult AddNewRecipe(int idmenu,int idrecipe) 
        {
            Menu M = K.Menu.Find(idmenu);
            if (check(M.listrecipe, idrecipe)) 
            {
                ViewBag.donerecipe = "This Recipe - "+K.Recipe.Find(idrecipe).name+" contains in menu - "+M.name;
                return MenuEditor(idmenu);
            }
            else
                
            M.listrecipe = recipeworker(M.listrecipe, idrecipe, true);
            K.Entry(M).State = EntityState.Modified;
            K.SaveChanges();
            return MenuEditor(idmenu);
        }

        private bool check(string listrecipe,int idrecipe) 
        {
            return listrecipe.Contains(Convert.ToString(idrecipe));
        }

        
    }
}
