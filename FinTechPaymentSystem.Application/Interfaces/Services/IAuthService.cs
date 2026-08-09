using FinTechPaymentSystem.Application.DTOs.Auth;

namespace FinTechPaymentSystem.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}