using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KH.Models;

namespace KH.Controllers
{
    public class RecipeController : Controller
    {
        //
        // GET: /Recipe/
        KHContext K = new KHContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AllRecipe() 
        {
            List<Recipe> ARecipe = K.Recipes.Where(u => u.id > 0).ToList();
            List<string> recipesname = new List<string>();
            List<string> recipedescription = new List<string>();
            List<int> recipeid = new List<int>();
            foreach (var i in ARecipe) 
            {
                recipeid.Add(i.id);
                recipesname.Add(i.name);
                recipedescription.Add(i.description);
            }
            ViewBag.recipename = recipesname;
            ViewBag.desc = recipedescription;
            ViewBag.recipeid = recipeid;
            int curid = Convert.ToInt32(Session["id"]);
            string menu = K.Users.Where(u => u.id == curid).FirstOrDefault().menulist;
            List<int> menuslist = menulist(menu);
            List<string> namemenu = new List<string>();
            foreach (var i in menuslist) 
            {
                namemenu.Add(K.Menus.Find(i).name);
            }
            ViewBag.namemenu = namemenu;
            ViewBag.menusid = menuslist;
            return View("../Resipe/Recipe");
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

        private List<string> convertor(string menu)
        {
            List<string> listmenu = new List<string>();
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
                listmenu.Add(idfriends);
                count = i + 1;
            }
            return listmenu;
        }

        [HttpPost]
        public ActionResult Search(string name) 
        {
            List<Recipe> SRecipe = K.Recipes.Where(r => r.name == name).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult NewRecipe(string name, List<Product> product) 
        {
            if (name == null || name == "") 
            {
                return View();
            }
            List<string> nameproduct = new List<string>();
            List<string> countproduct = new List<string>();
            List<string> countnameproduct = new List<string>();
            foreach (var i in product) 
            {
                nameproduct.Add(i.name);
                countproduct.Add(Convert.ToString(i.count));
                countnameproduct.Add(i.countname);
            }
            //Recipe R = new Recipe { name=name,
            //    product=convertstring(nameproduct),
            ////countprod=convertstring(countproduct),
            ////countname=convertstring(countnameproduct)};
            return AllRecipe();
        }

        private string convertstring(List<string> list) 
        {
            string newlist = "";
            foreach (var i in list) 
            {
                newlist += i + ';';
            }
            return newlist;
        }

        [HttpPost]
        public ActionResult Info(int idrecipe) 
        {
            Recipe R = K.Recipes.Find(idrecipe);
            List<int> idproduct = menulist(R.product);
            List<string> nameproduct = new List<string>();
            List<int> count = new List<int>();
            List<string> countname = new List<string>();
            foreach (var i in idproduct) 
            {
                nameproduct.Add(K.Products.Find(i).name);
                count.Add(K.Products.Find(i).count);
                countname.Add(K.Products.Find(i).countname);
            }
            ViewBag.idrecipe = idrecipe;
            ViewBag.name = R.name;
            ViewBag.desc = R.description;
            ViewBag.nameproduct = nameproduct;
            ViewBag.count = count;
            ViewBag.countname = countname;
            return View("../Resipe/RecipeInfo");
        }
    }
}
