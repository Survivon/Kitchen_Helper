using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.Models
{
    class KHContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Buylist> Buylist { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
