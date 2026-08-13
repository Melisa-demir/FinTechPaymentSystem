using FinTechPaymentSystem.Domain.Interfaces;
using FinTechPaymentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinTechPaymentSystem.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinTechDbContext _context;

        public TransactionRepository(FinTechDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(
            FinTechPaymentSystem.Domain.Entities.Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task<List<FinTechPaymentSystem.Domain.Entities.Transaction>>
            GetByWalletIdAsync(int walletId)
        {
            return await _context.Transactions
                .Where(x => x.WalletId == walletId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}