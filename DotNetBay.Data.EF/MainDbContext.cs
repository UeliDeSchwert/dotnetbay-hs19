using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Data.Entity;

namespace DotNetBay.Data.EF
{
    public class MainDbContext : System.Data.Entity.DbContext
    {
        public MainDbContext() : base("DatabaseConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Add(new DateTime2Convention());

            modelBuilder.Entity<Auction>().HasRequired(t => t.Seller).WithMany(k => k.Auctions);
            modelBuilder.Entity<Auction>().HasOptional(t => t.Winner);
            modelBuilder.Entity<Auction>().HasOptional(t => t.ActiveBid).WithRequired(k => k.Auction);
            modelBuilder.Entity<Auction>().HasMany(t => t.Bids);
        }

        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<Member> Members { get; set; }
    }
}
