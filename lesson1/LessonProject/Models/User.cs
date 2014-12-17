using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kitchen_Helper.Models
{
    class User
    {
        public int id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public int storage { get; set; }

        public string menulist { get; set; }
    }
}
