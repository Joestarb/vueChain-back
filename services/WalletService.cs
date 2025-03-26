using Microsoft.EntityFrameworkCore;
using vueChain.Data;
using vueChain.Dtos;
using vueChain.interfaces;
using vueChain.Models;

namespace vueChain.services
{
    public class WalletService : IWalletService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WalletService> _logger;

        public WalletService(ApplicationDbContext context, ILogger<WalletService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task UpdateWallet(int userId, string symbol, decimal quantity, string transactionType)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId && w.Symbol == symbol);

            if (wallet == null)
            {
                if (transactionType == "Buy")
                {
                    wallet = new Wallet
                    {
                        UserId = userId,
                        Symbol = symbol,
                        Quantity = quantity,
                        LastUpdated = DateTime.UtcNow
                    };
                    _context.Wallets.Add(wallet);
                    _logger.LogInformation($"Se añadió {quantity} de {symbol} a la cartera de usuario {userId}.");
                }
                else
                {
                    throw new Exception("No se puede vender una moneda que no existe en la cartera.");
                }
            }
            else
            {
                if (transactionType == "Buy")
                {
                    wallet.Quantity += quantity; // Agregar cantidad comprada
                }
                else if (transactionType == "Sell")
                {
                    if (wallet.Quantity >= quantity)
                    {
                        wallet.Quantity -= quantity; // Restar cantidad vendida
                    }
                    else
                    {
                        throw new Exception("Cantidad insuficiente en la cartera para realizar la venta.");
                    }
                }
                wallet.LastUpdated = DateTime.UtcNow;
                _logger.LogInformation($"Cartera actualizada: {symbol}, nueva cantidad: {wallet.Quantity} para usuario {userId}.");
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WalletDto>> GetWallet(int userId)
        {
            var wallet = _context.Wallets
                .Where(w => w.UserId == userId)
                .Select(w => new WalletDto
                {
                    Symbol = w.Symbol,
                    Quantity = w.Quantity
                }).ToList();

            return await Task.FromResult(wallet);
        }
    }

}
