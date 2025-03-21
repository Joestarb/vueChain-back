using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using vueChain.Dtos;
using vueChain.interfaces;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider; // Inyectamos IServiceProvider

    public LoggingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Permite que la solicitud pase al siguiente middleware
            await _next(context);

            // Crear un scope para resolver ILogService
            using (var scope = _serviceProvider.CreateScope())
            {
                var logService = scope.ServiceProvider.GetRequiredService<ILogService>();

                // Registrar log con el servicio
                var log = new LogDto
                {
                    Level = context.Response.StatusCode >= 500 ? "Error" : "Info",
                    Message = $"Endpoint: {context.Request.Method} {context.Request.Path} - Status: {context.Response.StatusCode}",
                    Source = "Server",
                    Details = $"User-Agent: {context.Request.Headers["User-Agent"]}, IP: {context.Connection.RemoteIpAddress}"
                };


                await logService.SaveLogAsync(log);
            }
        }
        catch (Exception ex)
        {
            // Capturar excepciones y registrar logs
            using (var scope = _serviceProvider.CreateScope())
            {
                var logService = scope.ServiceProvider.GetRequiredService<ILogService>();

                var log = new LogDto
                {
                    Level = "Error",
                    Message = $"Exception: {ex.Message} - Endpoint: {context.Request.Method} {context.Request.Path}",
                    Source = "Server",
                    Details = ex.StackTrace
                };

                await logService.SaveLogAsync(log);
            }

            throw; // Re-lanzar la excepción
        }
    }
}
