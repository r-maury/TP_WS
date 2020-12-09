using System;
using System.Data.Entity;
using System.Linq;
using TP_WebService.Models;

namespace TP_WebService.Repositories {
    public class MyDbContext : DbContext {
        public MyDbContext()
            : base("name=MyDbContext") {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}