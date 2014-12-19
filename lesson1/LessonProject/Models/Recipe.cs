using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.Models
{
    class Recipe
    {
        public int id { get; set; }

        public string name { get; set; }

        public string product { get; set; }

        public int countprod { get; set; }

        public string countname { get; set; }

        public DateTime date { get; set; }
    }
}
