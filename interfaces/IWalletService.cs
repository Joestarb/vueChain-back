using vueChain.Dtos;

namespace vueChain.interfaces
{
    public interface IWalletService
    {
        Task UpdateWallet(int userId, string symbol, decimal quantity, string transactionType);
        Task<IEnumerable<WalletDto>> GetWallet(int userId);
    }
}