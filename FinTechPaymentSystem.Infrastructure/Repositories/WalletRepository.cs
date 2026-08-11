using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinTechPaymentSystem.Domain.Entities;
using FinTechPaymentSystem.Domain.Interfaces;
using FinTechPaymentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinTechPaymentSystem.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly FinTechDbContext _context;
        public WalletRepository(FinTechDbContext context)
        {
            _context = context;
        }
        public async Task<Wallet?> GetByUserIdAsync(int userId)
        {
            return await _context.Wallets
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task AddAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
