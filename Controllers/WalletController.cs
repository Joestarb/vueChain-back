using Microsoft.AspNetCore.Mvc;
using vueChain.interfaces;
using vueChain.Requests;

namespace vueChain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateWallet([FromBody] UpdateWalletRequest request)
        {
            try
            {
                await _walletService.UpdateWallet(request.UserId, request.Symbol, request.Quantity, request.TransactionType);
                return Ok("Cartera actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWallet(int userId)
        {
            var wallet = await _walletService.GetWallet(userId);
            return Ok(wallet);
        }
    }

}
