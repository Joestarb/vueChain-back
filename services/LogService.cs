using Microsoft.EntityFrameworkCore;
using System;
using vueChain.Data;
using vueChain.Dtos;
using vueChain.interfaces;
using vueChain.Models;

namespace vueChain.services
{
    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _context;

        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Log>> GetAllLogsAsync()
        {
            return await _context.Logs.ToListAsync();
        }

        public async Task SaveLogAsync(LogDto logDto)
        {
            if (logDto == null)
                throw new ArgumentNullException(nameof(logDto));

            var log = new Log
            {
                Level = logDto.Level,
                Message = logDto.Message,
                Source = logDto.Source,
                Details = logDto.Details,
                Date = DateTime.UtcNow
            };

            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }

}
