using Binance.Net.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using vueChain.Dtos;


[ApiController]
[Route("[controller]")]
public class BinanceController : ControllerBase
{
    private readonly BinanceService _binanceService;

    public BinanceController(BinanceService binanceService)
    {
        _binanceService = binanceService;
    }

    [HttpPost("buy")]
    public async Task<IActionResult> SimulateBuyOrder(string symbol, decimal quantity, decimal price)
    {
        var order = await _binanceService.SimulateBuyOrder(symbol, quantity, price);
        return Ok(order);
    }

    [HttpPost("sell")]
    public async Task<IActionResult> SimulateSellOrder(string symbol, decimal quantity, decimal price)
    {
        var order = await _binanceService.SimulateSellOrder(symbol, quantity, price);
        return Ok(order);
    }

    [HttpPost("alert")]
    public async Task<IActionResult> SetPriceAlert(string symbol, decimal targetPrice)
    {
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
        var sellers = await _binanceService.GetP2PSellers(symbol);
        return Ok(sellers);
    }

    [HttpPost("simulate-p2p-buy")]
    public async Task<IActionResult> SimulateP2PBuyOrder([FromBody] P2PBuyOrderDto orderDto)
    {
        try
        {
            var result = await _binanceService.SimulateP2PBuyOrder(orderDto.Symbol, orderDto.Quantity, orderDto.Price);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions()
    {
        var transactions = await _binanceService.GetTransactions();
        return Ok(transactions);
    }

    [HttpGet("price")]
    public async Task<IActionResult> GetRealTimePrice(string symbol)
    {
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