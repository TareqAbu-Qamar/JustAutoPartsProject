using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JustAutoPartsProject.Models
{
    public class modelContext : DbContext
    {
        public modelContext() : base("name=JustAutoPartsDB")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<VirtualWallet> VirtualWallets { get; set; }
    }
}