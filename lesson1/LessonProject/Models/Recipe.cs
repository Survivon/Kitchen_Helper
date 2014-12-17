using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Kitchen_Helper.Models
{
    class Recipe
    {
        public int id { get; set; }

        public string name { get; set; }

        public string productname { get; set; }

        public int countproduct { get; set; }

        public string countname { get; set; }
    }
}
