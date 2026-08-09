using FinTechPaymentSystem.Domain.Entities;

namespace FinTechPaymentSystem.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task SaveChangesAsync();
    }
}