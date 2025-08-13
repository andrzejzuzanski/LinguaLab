using LinguaLab.Application.DTOs;
using LinguaLab.Application.Interfaces;
using LinguaLab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Infrastructure.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly LinguaLabDbContext _context;
        public AnalyticsRepository(LinguaLabDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ActivityHeatmapDto>> GetUserActivityByDayAsync(Guid userId)
        {
            return await _context.ReviewLogs
                .Where(rl => rl.UserId == userId)
                .GroupBy(rl => rl.ReviewTimestamp.Date)
                .Select(g => new ActivityHeatmapDto
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
        }
    }
}
