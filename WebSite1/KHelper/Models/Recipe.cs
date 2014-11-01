using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KHelper.Models
{
    public class Recipe
    {
        public List<string> nameofproduct { get; set; }

        public List<int> count { get; set; }

        public List<string> title { get; set; }

        public string photo { get; set; }

        public string nameofrecipe { get; set; }

        public string info { get; set; }
    }
}