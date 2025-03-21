namespace vueChain.Interfaces
{
    public interface ITradingViewService
    {
        Task<string> GetTradingViewDataAsync();
        Task<string> GetCriptoCurrency();
         Task<string>  GetCompanies();
        Task<string>  GetCompanies();
    }
}