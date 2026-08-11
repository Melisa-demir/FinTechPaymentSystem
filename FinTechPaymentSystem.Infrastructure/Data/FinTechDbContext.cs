using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinTechPaymentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTechPaymentSystem.Infrastructure.Data
{

    public class FinTechDbContext : DbContext
    {
        public FinTechDbContext(DbContextOptions<FinTechDbContext> options) : base(options)
        {
        }
    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithOne(u => u.Wallet)
                .HasForeignKey<Wallet>(w => w.UserId);

            modelBuilder.Entity<Wallet>()
                .Property(x => x.Balance)
                .HasPrecision(18, 2);
        }
    }
}