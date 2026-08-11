using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinTechPaymentSystem.Application.DTOs.Wallet;

namespace FinTechPaymentSystem.Application.Interfaces.Services
{
    public interface IWalletService
    {
        Task<WalletResponse> GetMyWalletAsync(int userId);
        Task<WalletResponse> DepositAsync(int userId, decimal amount);
    }
}
