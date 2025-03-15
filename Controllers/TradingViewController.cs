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

        [HttpGet("bolsa")]
        [Authorize]
        public async Task<IActionResult> GetTradingViewData()
        {
            var data = await _tradingViewService.GetTradingViewDataAsync();
            return Content(data, "application/javascript");
        }
        
        [HttpGet("cripto-currency")]
        [Authorize]
        public async Task<IActionResult> GetCriptoCurrency()
        {
            var data = await _tradingViewService.GetCriptoCurrency();
            return Content(data, "application/javascript");
        }
        
        [HttpGet("get-companies")]
        [Authorize]
        public async Task<IActionResult>GetCompanies()
        {
            var data = await _tradingViewService.GetCompanies();
            return Content(data, "application/javascript");
        }
    }
}