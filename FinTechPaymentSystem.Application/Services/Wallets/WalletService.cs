using FinTechPaymentSystem.Application.DTOs.Wallet;
using FinTechPaymentSystem.Application.Interfaces.Services;
using FinTechPaymentSystem.Domain.Interfaces;

namespace FinTechPaymentSystem.Application.Services.Wallets
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<WalletResponse> GetMyWalletAsync(int userId)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(userId);

            if (wallet is null)
            {
                throw new Exception("Wallet not found");
            }

            return new WalletResponse
            {
                Id = wallet.Id,
                Balance = wallet.Balance
            };
        }
    }
}