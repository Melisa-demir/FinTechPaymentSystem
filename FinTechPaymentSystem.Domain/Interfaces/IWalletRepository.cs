using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinTechPaymentSystem.Domain.Entities;

namespace FinTechPaymentSystem.Domain.Interfaces
{
    public interface IWalletRepository
    {
        Task<Wallet?> GetByUserIdAsync(int userId);
        Task AddAsync(Wallet wallet);
        Task SaveChangesAsync();
    }
}
