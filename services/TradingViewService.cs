using System.Net.Http;
using System.Threading.Tasks;
using vueChain.Interfaces;

namespace vueChain.Services
{
    public class TradingViewService : ITradingViewService
    {
        private readonly HttpClient _httpClient;

        public TradingViewService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTradingViewDataAsync()
        {
            string url = "https://s3.tradingview.com/tv.js";
            var response = await _httpClient.GetStringAsync(url);
            return response;
        }
    }
}