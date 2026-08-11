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

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(WithdrawRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdClaim.Value);

            var wallet = await _walletService.WithdrawAsync(
                userId,
                request.Amount);

            return Ok(wallet);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(TransferRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                return Unauthorized();
            }

            var senderUserId = int.Parse(userIdClaim.Value);

            var wallet = await _walletService.TransferAsync(
                senderUserId,
                request.ReceiverUserId,
                request.Amount);

            return Ok(wallet);
        }
    }
}