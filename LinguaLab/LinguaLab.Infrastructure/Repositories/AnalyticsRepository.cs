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

        public async Task<IEnumerable<CategoryAccuracyDto>> GetAccuracyByCategoryAsync(Guid userId)
        {
            return await _context.ReviewLogs
                .Where(rl => rl.UserId == userId)
                .Include(rl => rl.Word)
                .ThenInclude(w => w.Category)
                .GroupBy(rl => rl.Word.Category.Name)
                .Select(g => new CategoryAccuracyDto
                {
                    CategoryName = g.Key,
                    Accuracy = g.Average(rl => rl.Quality) /5.0 * 100.0
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ActivityHeatmapDto>> GetLearnedWordsByDayAsync(Guid userId)
        {
            var allLogs = await _context.ReviewLogs
                    .Where(rl => rl.UserId == userId)
                    .OrderBy(rl => rl.ReviewTimestamp)
                    .ToListAsync();

            var result = allLogs
                    .GroupBy(rl => rl.WordId)
                    .Select(g => g.First())
                    .GroupBy(firstReview => firstReview.ReviewTimestamp.Date)
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
