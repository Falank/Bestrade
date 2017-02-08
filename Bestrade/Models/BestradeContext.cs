using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    public class BestradeContext:DbContext
    {
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Mod> Mods { get; set; }
        public DbSet<FBA> FBA { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Pack> Packs { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}