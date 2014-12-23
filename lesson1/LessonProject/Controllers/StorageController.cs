using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KH.Models;

namespace KH.Controllers
{
    public class StorageController : Controller
    {
        //
        // GET: /Storage/
        KHContext K = new KHContext();
        MainController MC = new MainController();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Del(int idproduct) 
        {
            Product p = K.Products.Find(idproduct);
            K.Products.Remove(p);
            K.SaveChanges();
            return MC.MainLoad(Convert.ToInt32(Session["id"]));
        }

        [HttpPost]
        public ActionResult cook(int idrecipe) 
        {
            Recipe R = K.Recipes.Find(idrecipe);
            List<int> recipename = menulist(R.product);
            List<string> prodname = new List<string>();
            List<int> count = new List<int>();
            List<string> countname = new List<string>();
            foreach (var i in recipename) 
            {
                prodname.Add(K.Products.Find(i).name);
                count.Add(K.Products.Find(i).count);
                countname.Add(K.Products.Find(i).countname);
            }
            Storage S = K.Storages.Find(Convert.ToInt32(Session["id"]));
            List<int> productinstorageid = menulist(S.nameproduct);
            List<string> nameproductinstorage = new List<string>();
            List<int> countproductinstorage = new List<int>();
            List<string> countnameinstorage = new List<string>();
            foreach (var i in productinstorageid) 
            {
                nameproductinstorage.Add(K.Products.Find(i).name);
                countproductinstorage.Add(K.Products.Find(i).count);
                countnameinstorage.Add(K.Products.Find(i).countname);
            }
            List<Product> newlist = new List<Product>();
            int i1 = 0;
            int j1 = 0;
            bool check = false;
            foreach (var i in prodname) 
            {
                foreach (var j in nameproductinstorage) 
                {
                    int newcount = 0;
                    if (i == j) 
                    {
                        if (countproductinstorage[j1] >= count[i1]) 
                        {
                            check = true;
                            break;
                        }
                        newcount = Math.Abs( countproductinstorage[j1] - count[i1]);
                        Product P = new Product() { name=i,count=newcount,countname=countname[i1]};
                        newlist.Add(P);
                        check = true;
                        break;
                    }

                    j1++;
                }
                j1 = 0;
                if (check == false) 
                {
                    Product P = new Product() { name=i,
                    count=count[i1],
                    countname=countname[i1]};
                    newlist.Add(P);
                }
                i1++;
            }
            ViewBag.product = newlist;
            return View("../Buylist/RecipeBuylist");
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
    }
}
