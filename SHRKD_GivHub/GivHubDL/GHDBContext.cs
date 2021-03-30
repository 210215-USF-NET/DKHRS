using GivHubModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GivHubDL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
        public DbSet<Location> Locations { get; set; }
        public DbSet<Charity> Charities { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Follow> Follows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Charity>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Charity>()
                .HasOne(charity => charity.Location)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

      

            modelBuilder.Entity<Donation>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<SearchHistory>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Subscription>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Follow>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
