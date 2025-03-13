using Binance.Net;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.SpotData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class BinanceService
{
    private readonly BinanceClient _client;
    private readonly ILogger<BinanceService> _logger;

    public BinanceService(IConfiguration configuration, ILogger<BinanceService> logger)
    {
        var apiKey = configuration["Binance:ApiKey"];
        var apiSecret = configuration["Binance:ApiSecret"];
        _client = new BinanceClient(new BinanceClientOptions
        {
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(apiKey, apiSecret),
            BaseAddress = "https://testnet.binance.vision" // URL del Testnet de Binance
        });
        _logger = logger;
    }

    public async Task<BinancePlacedOrder> SimulateBuyOrder(string symbol, decimal quantity, decimal price)
    {
        var result = await _client.Spot.Order.PlaceOrderAsync(symbol, Binance.Net.Enums.OrderSide.Buy, Binance.Net.Enums.OrderType.Limit, quantity, price: price);
        if (result.Success)
        {
            _logger.LogInformation($"Orden de compra simulada: {result.Data}");
            return result.Data;
        }
        else
        {
            _logger.LogError($"Error al simular la orden de compra: {result.Error}");
            throw new Exception(result.Error.Message);
        }
    }

    public async Task<BinancePlacedOrder> SimulateSellOrder(string symbol, decimal quantity, decimal price)
    {
        var result = await _client.Spot.Order.PlaceOrderAsync(symbol, Binance.Net.Enums.OrderSide.Sell, Binance.Net.Enums.OrderType.Limit, quantity, price: price);
        if (result.Success)
        {
            _logger.LogInformation($"Orden de venta simulada: {result.Data}");
            return result.Data;
        }
        else
        {
            _logger.LogError($"Error al simular la orden de venta: {result.Error}");
            throw new Exception(result.Error.Message);
        }
    }

    public async Task SetPriceAlert(string symbol, decimal targetPrice, Action<decimal> onPriceReached)
    {
        var ticker = await _client.Spot.Market.GetPriceAsync(symbol);
        if (ticker.Success)
        {
            if (ticker.Data.Price >= targetPrice)
            {
                onPriceReached(ticker.Data.Price);
            }
        }
        else
        {
            _logger.LogError($"Error al obtener el precio del símbolo {symbol}: {ticker.Error}");
            throw new Exception(ticker.Error.Message);
        }
    }
}
