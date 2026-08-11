using FinTechPaymentSystem.Application.DTOs.Wallet;
using FinTechPaymentSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinTechPaymentSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyWallet()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdClaim.Value);

            var wallet = await _walletService.GetMyWalletAsync(userId);

            return Ok(wallet);
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(DepositRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdClaim.Value);

            var wallet = await _walletService.DepositAsync(
                userId,
                request.Amount);

            return Ok(wallet);
        }
    }
}