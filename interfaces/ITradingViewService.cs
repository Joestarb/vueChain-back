namespace vueChain.Interfaces
{
    public interface ITradingViewService
    {
        Task<string> GetTradingViewDataAsync();
    }
}