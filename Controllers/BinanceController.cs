using Binance.Net.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using vueChain.Dtos;

[ApiController]
[Route("[controller]")]
public class BinanceController : ControllerBase
{
    private readonly BinanceService _binanceService;
    private readonly ILogger<BinanceController> _logger;

    public BinanceController(BinanceService binanceService, ILogger<BinanceController> logger)
    {
        _binanceService = binanceService;
        _logger = logger;
    }

    [HttpPost("buy")]
    public async Task<IActionResult> SimulateBuyOrder(string symbol, decimal quantity, decimal price, int userId)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        _logger.LogInformation($"SimulateBuyOrder request from IP: {ipAddress}");

        await _binanceService.SimulateBuyOrder(symbol, quantity, price, userId);
        return Ok(new { message = "Orden de compra simulada guardada." });
    }

    [HttpPost("sell")]
    public async Task<IActionResult> SimulateSellOrder(string symbol, decimal quantity, decimal price, int userId)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        _logger.LogInformation($"SimulateSellOrder request from IP: {ipAddress}");

        await _binanceService.SimulateSellOrder(symbol, quantity, price, userId);
        return Ok(new { message = "Orden de venta simulada guardada." });
    }

    [HttpPost("alert")]
    public async Task<IActionResult> SetPriceAlert(string symbol, decimal targetPrice)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        _logger.LogInformation($"SetPriceAlert request from IP: {ipAddress}");

        await _binanceService.SetPriceAlert(symbol, targetPrice, price =>
        {
            // Lógica para manejar la alerta de precio
            Console.WriteLine($"El precio de {symbol} ha alcanzado {price}");
        });
        return Ok();
    }

    [HttpGet("p2p/sellers")]
    public async Task<IActionResult> GetP2PSellers(string symbol)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        _logger.LogInformation($"GetP2PSellers request from IP: {ipAddress}");

        var sellers = await _binanceService.GetP2PSellers(symbol);
        return Ok(sellers);
    }

    [HttpPost("simulate-p2p-buy")]
    public async Task<IActionResult> SimulateP2PBuyOrder([FromBody] P2PBuyOrderDto orderDto, int userId)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        _logger.LogInformation($"SimulateP2PBuyOrder request from IP: {ipAddress}");

        try
        {
            await _binanceService.SimulateP2PBuyOrder(orderDto.Symbol, orderDto.Quantity, orderDto.Price, userId);
            return Ok(new { message = "Orden de compra P2P simulada guardada." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions()
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        _logger.LogInformation($"GetTransactions request from IP: {ipAddress}");

        var transactions = await _binanceService.GetTransactions();
        return Ok(transactions);
    }

    [HttpGet("price")]
    public async Task<IActionResult> GetRealTimePrice(string symbol)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        _logger.LogInformation($"GetRealTimePrice request from IP: {ipAddress}");

        try
        {
            var price = await _binanceService.GetRealTimePrice(symbol);
            return Ok(price);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("historical-trends")]
    public async Task<IActionResult> GetHistoricalTrends(string symbol, KlineInterval interval, DateTime startTime, DateTime endTime)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        _logger.LogInformation($"GetHistoricalTrends request from IP: {ipAddress}");

        try
        {
            var trends = await _binanceService.GetHistoricalTrends(symbol, interval, startTime, endTime);
            return Ok(trends);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

