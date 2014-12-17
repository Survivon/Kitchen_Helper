using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Kitchen_Helper.Models
{
    class KHDBContext : DbContext
    {
        public List<User> Users { get; set; }

        public List<Storage> Storages { get; set; }

        public List<Recipe> Recipes { get; set; }

        public List<Menu> Menus { get; set; }

        public List<Buylist> Buylists { get; set; }

    }
}
