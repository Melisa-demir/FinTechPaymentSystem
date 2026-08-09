using FinTechPaymentSystem.Application.DTOs.Auth;
using FinTechPaymentSystem.Application.Interfaces.Services;
using FinTechPaymentSystem.Domain.Entities;
using FinTechPaymentSystem.Domain.Interfaces;

namespace FinTechPaymentSystem.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var existingUser =
                await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            var passwordHash =
                BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user =
                await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception("Email or password is incorrect");
            }

            var passwordIsValid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash);

            if (!passwordIsValid)
            {
                throw new Exception("Email or password is incorrect");
            }

            var token = _tokenService.GenerateToken(user);

            return new LoginResponse
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            };
        }
    }
}