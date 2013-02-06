using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExternalBadass.Models
{
    public class BadassContext : DbContext
    {
        static BadassContext()
        {
            //Database.SetInitializer<BadassContext>(null);
        }

        public BadassContext()
            : base("Name=BadassContext")
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Incentive> Incentives { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}