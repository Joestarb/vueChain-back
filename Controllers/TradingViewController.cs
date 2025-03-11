using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using vueChain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace vueChain.Controllers
{
    [ApiController]
    [Route("api/tradingview")]
    public class TradingViewController : ControllerBase
    {
        private readonly ITradingViewService _tradingViewService;

        public TradingViewController(ITradingViewService tradingViewService)
        {
            _tradingViewService = tradingViewService;
        }

        [HttpGet("data")]
        [Authorize]
        public async Task<IActionResult> GetTradingViewData()
        {
            var data = await _tradingViewService.GetTradingViewDataAsync();
            return Content(data, "application/javascript");
        }
    }
}