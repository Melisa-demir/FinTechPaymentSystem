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
    }
}