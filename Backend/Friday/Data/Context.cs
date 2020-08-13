using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;
using Friday.Models.Logs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data
{
    public class Context : IdentityDbContext
    {

        public DbSet<Item> Items { get; set; }
        public DbSet<ShopUser> ShopUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemDetails> ItemDetails { get; set; }
        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<CurrencyLog> CurrencyLogs { get; set; }
        public DbSet<ItemLog> ItemLogs { get; set; }

        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Item>();

            builder.Entity<ItemDetails>();

            builder.Entity<ShopUser>();

            builder.Entity<Order>().HasOne(t => t.User).WithMany(t => t.Orders);

            builder.Entity<OrderItem>().HasKey(s => new { s.OrderId, s.ItemId });

            builder.Entity<Configuration>();

            builder.Entity<CurrencyLog>();

        }
    }
}
