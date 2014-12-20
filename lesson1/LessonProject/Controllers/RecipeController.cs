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
            List<Recipe> ARecipe = K.Recipe.Where(u => u.id > 0).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Search(string name) 
        {
            List<Recipe> SRecipe = K.Recipe.Where(r => r.name == name).ToList();

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
            Recipe R = new Recipe { name=name,
                product=convertstring(nameproduct),
            countprod=convertstring(countproduct),
            countname=convertstring(countnameproduct)};
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
    }
}
