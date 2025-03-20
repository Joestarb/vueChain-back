using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.SpotData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vueChain.Dtos;
using vueChain.Models;
using vueChain.Data;

public class BinanceService
{
    private readonly BinanceClient _client;
    private readonly ILogger<BinanceService> _logger;
    private readonly ApplicationDbContext _context;

    public BinanceService(IConfiguration configuration, ILogger<BinanceService> logger, ApplicationDbContext context)
    {
        var apiKey = configuration["Binance:ApiKey"];
        var apiSecret = configuration["Binance:ApiSecret"];
        _client = new BinanceClient(new BinanceClientOptions
        {
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(apiKey, apiSecret),
            BaseAddress = "https://testnet.binance.vision" // URL del Testnet de Binance
        });
        _logger = logger;
        _context = context;
    }

    public async Task SimulateBuyOrder(string symbol, decimal quantity, decimal price, int userId)
    {
        // Guardar la transacción en la tabla de transacciones
        await LogTransaction(symbol, quantity, price, "Buy", userId);
        _logger.LogInformation($"Orden de compra simulada guardada: {symbol}, {quantity}, {price}");
    }

    public async Task SimulateSellOrder(string symbol, decimal quantity, decimal price, int userId)
    {
        // Guardar la transacción en la tabla de transacciones
        await LogTransaction(symbol, quantity, price, "Sell", userId);
        _logger.LogInformation($"Orden de venta simulada guardada: {symbol}, {quantity}, {price}");
    }

    public async Task SetPriceAlert(string symbol, decimal targetPrice, Action<decimal> onPriceReached)
    {
        var ticker = await _client.Spot.Market.GetPriceAsync(symbol);
        if (ticker.Success)
        {
            if (ticker.Data.Price >= targetPrice)
            {
                onPriceReached(ticker.Data.Price);
                await LogTransaction(symbol, 0, targetPrice, "Alert", 0); // Asumimos que no hay usuario para alertas
            }
        }
        else
        {
            _logger.LogError($"Error al obtener el precio del símbolo {symbol}: {ticker.Error}");
            throw new Exception(ticker.Error.Message);
        }
    }

    private async Task LogTransaction(string symbol, decimal quantity, decimal price, string type, int userId)
    {
        var transaction = new TransactionsModel
        {
            Symbol = symbol,
            Quantity = quantity,
            Price = price,
            Type = type,
            Timestamp = DateTime.UtcNow,
            UserId = userId
        };
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    // Método para obtener la lista de usuarios haciendo ventas P2P
    public async Task<IEnumerable<UserP2PInfo>> GetP2PSellers(string symbol)
    {
        // Supongamos que hay un endpoint en la API de Binance para obtener esta información
        var result = await _client.Spot.Market.GetOrderBookAsync(symbol);
        if (result.Success)
        {
            var sellers = result.Data.Asks.Select(a => new UserP2PInfo
            {
                Price = a.Price,
                Quantity = a.Quantity,
                UserName = "UsuarioSimulado" // Simulamos que obtenemos el nombre del usuario
            }).ToList();
            _logger.LogInformation($"Lista de usuarios haciendo ventas P2P: {string.Join(", ", sellers.Select(s => s.UserName))}");
            return sellers;
        }
        else
        {
            _logger.LogError($"Error al obtener la lista de usuarios haciendo ventas P2P: {result.Error}");
            throw new Exception(result.Error.Message);
        }
    }

    public async Task<IEnumerable<TransactionsDto>> GetTransactions()
    {
        var transactions = _context.Transactions.Select(t => new TransactionsDto
        {
            Symbol = t.Symbol,
            Quantity = t.Quantity,
            Price = t.Price,
            Type = t.Type,
            Timestamp = t.Timestamp
        });
        return await Task.FromResult(transactions);
    }

    public async Task SimulateP2PBuyOrder(string symbol, decimal quantity, decimal price, int userId)
    {
        // Obtener datos del vendedor de la API de Binance
        var sellers = await GetP2PSellers(symbol);
        var seller = sellers.FirstOrDefault();

        if (seller != null)
        {
            _logger.LogInformation($"Datos del vendedor obtenidos: {seller.UserName}");

            // Guardar la transacción en la tabla de transacciones
            await LogTransaction(symbol, quantity, price, "P2PBuy", userId);
        }
        else
        {
            _logger.LogError("No se encontraron vendedores para el símbolo especificado.");
            throw new Exception("No se encontraron vendedores para el símbolo especificado.");
        }
    }

    public async Task<decimal> GetRealTimePrice(string symbol)
    {
        var result = await _client.Spot.Market.GetPriceAsync(symbol);
        if (result.Success)
        {
            return result.Data.Price;
        }
        else
        {
            _logger.LogError($"Error al obtener el precio en tiempo real del símbolo {symbol}: {result.Error}");
            throw new Exception(result.Error.Message);
        }
    }

    public async Task<IEnumerable<CandlestickDto>> GetHistoricalTrends(string symbol, KlineInterval interval, DateTime startTime, DateTime endTime)
    {
        var result = await _client.Spot.Market.GetKlinesAsync(symbol, interval, startTime, endTime);
        if (result.Success)
        {
            var trends = result.Data.Select(k => new CandlestickDto
            {
                OpenTime = k.OpenTime,
                Open = k.Open,
                High = k.High,
                Low = k.Low,
                Close = k.Close,
                BaseVolume = k.BaseVolume,
                CloseTime = k.CloseTime
            });
            return trends;
        }
        else
        {
            _logger.LogError($"Error al obtener las tendencias históricas del símbolo {symbol}: {result.Error}");
            throw new Exception(result.Error.Message);
        }
    }
}

public class UserP2PInfo
{
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public string UserName { get; set; }
}