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

        public async Task<IEnumerable<ActivityHeatmapDto>> GetLearnedWordsByDayAsync(Guid userId)
        {
            var allLogs = await _context.ReviewLogs
                    .Where(rl => rl.UserId == userId)
                    .OrderBy(rl => rl.ReviewTimestamp)
                    .ToListAsync();

            var result = allLogs
                    .GroupBy(rl => rl.WordId) // Najpierw grupujemy wszystkie logi po WordId
                    .Select(g => g.First())   // Z każdej grupy bierzemy tylko pierwszy log (najwcześniejszą odpowiedź)
                    .GroupBy(firstReview => firstReview.ReviewTimestamp.Date) // Teraz grupujemy te "pierwsze odpowiedzi" po dacie
                    .Select(finalGroup => new ActivityHeatmapDto
                    {
                        Date = finalGroup.Key,
                        Count = finalGroup.Count()
                    })
                    .ToList();

            return result;
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
