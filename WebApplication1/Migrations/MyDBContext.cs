using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Migrations
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("MyConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Faktura> Faktury { get; set; }
        public DbSet<FakturaPozycja> FakturaPozycja { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FakturaPozycja>().Property(x => x.WartoscBrutto).HasPrecision(18, 3);
        }
    }
}