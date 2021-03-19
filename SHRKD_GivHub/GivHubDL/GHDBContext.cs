using GivHubModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GivHubDL;

namespace GivHubDL
{
    public class GHDBContext : DbContext
    {
        public GHDBContext(DbContextOptions options) : base(options)
        {
        }

        public GHDBContext()
        {
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Charity> Charities { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
