using Microsoft.AspNetCore.Mvc;
using vueChain.Dtos;
using vueChain.interfaces;

namespace vueChain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ILogger<LogsController> _logger;
        private readonly ILogService _logService;

        public LogsController(ILogger<LogsController> logger, ILogService logService)
        {
            _logger = logger;
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogs()
        {
            try
            {
                var logs = await _logService.GetAllLogsAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving logs: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving the logs.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveLog([FromBody] LogDto logDto)
        {
            if (logDto == null) return BadRequest("The log data cannot be null.");

            try
            {
                await _logService.SaveLogAsync(logDto);
                _logger.LogInformation($"Log recorded: {logDto.Message}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving log: {ex.Message}");
                return StatusCode(500, "An error occurred while saving the log.");
            }
        }
    }

}
