using FinTechPaymentSystem.Domain.Entities;
using FinTechPaymentSystem.Domain.Interfaces;
using FinTechPaymentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinTechPaymentSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinTechDbContext _context;

        public UserRepository(FinTechDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}