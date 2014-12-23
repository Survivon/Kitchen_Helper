using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KH.Models;
using System.Data;

namespace KHMobile.Controllers
{
    public class StorageController : Controller
    {
        //
        // GET: /Storage/
        KHContext K = new KHContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadStorage(int id) 
        {
            if(id==null)
                 id = Convert.ToInt32(Session["id"]);
            Storage S = K.Storages.Find(id);
            string productid = S.nameproduct;
            List<Product> P = new List<Product>();
            foreach (var i in convert(productid)) 
            {
                P.Add(K.Products.Find(i));
            }
            ViewBag.Product = P;
            return View("../Storage/Storage");
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
        public ActionResult Add(string productname, int count,string countname) 
        {
            Product P = new Product() { name=productname,count=count,countname=countname};
            K.Products.Add(P);
            K.SaveChanges();
            List<Product> product = K.Products.Where(p => p.name == productname).ToList();
            int idproduct = 0;
            foreach (var i in product) 
            {
                if (i.count == count)
                    idproduct = i.id;
            }
            Storage S = K.Storages.Find(Convert.ToInt32(Session["id"]));
            S.nameproduct += Convert.ToString(idproduct);
            S.nameproduct += ';';
            K.Entry(S).State = EntityState.Modified;
            K.SaveChanges();
            return LoadStorage(Convert.ToInt32(Session["id"]));
        }

        [HttpPost]
        public ActionResult Del(int idproduct)
        {
            Product p = K.Products.Find(idproduct);
            K.Products.Remove(p);
            K.SaveChanges();
            return LoadStorage(Convert.ToInt32(Session["id"]));
        }
    }
}
