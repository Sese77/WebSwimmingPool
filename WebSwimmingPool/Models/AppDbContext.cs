using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebSwimmingPool.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(): base("Sesethu") { }
        public DbSet<Pools> pools { get; set; }
        public DbSet<TimeClass> timeClass { get; set; }
        public DbSet<Bookings> bookings { get; set; }
    }
}