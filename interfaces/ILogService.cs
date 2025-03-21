using vueChain.Dtos;
using vueChain.Models;

namespace vueChain.interfaces
{
    public interface ILogService
    {
        Task<List<Log>> GetAllLogsAsync();
        Task SaveLogAsync(LogDto logDto);
    }
}
