namespace FinTechPaymentSystem.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddAsync(
            FinTechPaymentSystem.Domain.Entities.Transaction transaction);

        Task<List<FinTechPaymentSystem.Domain.Entities.Transaction>>
            GetByWalletIdAsync(int walletId);

        Task SaveChangesAsync();
    }
}