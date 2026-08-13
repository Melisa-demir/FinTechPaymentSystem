using FinTechPaymentSystem.Application.DTOs.Wallet;
using FinTechPaymentSystem.Application.Interfaces.Services;
using FinTechPaymentSystem.Domain.Interfaces;

namespace FinTechPaymentSystem.Application.Services.Wallets
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        public WalletService(
            IWalletRepository walletRepository,
            ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
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

        public async Task<WalletResponse> DepositAsync(
            int userId,
            decimal amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Amount must be greater than zero");
            }

            var wallet = await _walletRepository.GetByUserIdAsync(userId);

            if (wallet is null)
            {
                throw new Exception("Wallet not found");
            }

            wallet.Balance += amount;

            var transaction =
                new FinTechPaymentSystem.Domain.Entities.Transaction
                {
                    WalletId = wallet.Id,
                    Amount = amount,
                    Type = "Deposit"
                };

            await _transactionRepository.AddAsync(transaction);

            await _walletRepository.SaveChangesAsync();

            return new WalletResponse
            {
                Id = wallet.Id,
                Balance = wallet.Balance
            };
        }

        public async Task<WalletResponse> WithdrawAsync(
            int userId,
            decimal amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Amount must be greater than zero");
            }

            var wallet = await _walletRepository.GetByUserIdAsync(userId);

            if (wallet is null)
            {
                throw new Exception("Wallet not found");
            }

            if (wallet.Balance < amount)
            {
                throw new Exception("Insufficient balance");
            }

            wallet.Balance -= amount;

            var transaction =
                new FinTechPaymentSystem.Domain.Entities.Transaction
                {
                    WalletId = wallet.Id,
                    Amount = amount,
                    Type = "Withdraw"
                };

            await _transactionRepository.AddAsync(transaction);

            await _walletRepository.SaveChangesAsync();

            return new WalletResponse
            {
                Id = wallet.Id,
                Balance = wallet.Balance
            };
        }

        public async Task<WalletResponse> TransferAsync(
            int senderUserId,
            int receiverUserId,
            decimal amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Amount must be greater than zero");
            }

            if (senderUserId == receiverUserId)
            {
                throw new Exception(
                    "You cannot transfer money to yourself");
            }

            var senderWallet =
                await _walletRepository.GetByUserIdAsync(senderUserId);

            if (senderWallet is null)
            {
                throw new Exception("Sender wallet not found");
            }

            var receiverWallet =
                await _walletRepository.GetByUserIdAsync(receiverUserId);

            if (receiverWallet is null)
            {
                throw new Exception("Receiver wallet not found");
            }

            if (senderWallet.Balance < amount)
            {
                throw new Exception("Insufficient balance");
            }

            senderWallet.Balance -= amount;
            receiverWallet.Balance += amount;

            var senderTransaction =
                new FinTechPaymentSystem.Domain.Entities.Transaction
                {
                    WalletId = senderWallet.Id,
                    RelatedWalletId = receiverWallet.Id,
                    Amount = amount,
                    Type = "TransferOut"
                };

            var receiverTransaction =
                new FinTechPaymentSystem.Domain.Entities.Transaction
                {
                    WalletId = receiverWallet.Id,
                    RelatedWalletId = senderWallet.Id,
                    Amount = amount,
                    Type = "TransferIn"
                };

            await _transactionRepository.AddAsync(senderTransaction);
            await _transactionRepository.AddAsync(receiverTransaction);

            await _walletRepository.SaveChangesAsync();

            return new WalletResponse
            {
                Id = senderWallet.Id,
                Balance = senderWallet.Balance
            };
        }
    }
}