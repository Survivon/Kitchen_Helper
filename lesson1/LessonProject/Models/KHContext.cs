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
        public DbSet<Storage> Storage { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Buylist> Buylist { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
