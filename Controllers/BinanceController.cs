using Microsoft.AspNetCore.Mvc;

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
}