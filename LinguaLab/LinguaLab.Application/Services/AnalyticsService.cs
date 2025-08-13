using LinguaLab.Application.DTOs;
using LinguaLab.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;
        public AnalyticsService(IAnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }
        public async Task<IEnumerable<ActivityHeatmapDto>> GetUserActivityHeatmapAsync(Guid userId)
        {
            return await _analyticsRepository.GetUserActivityByDayAsync(userId);
        }
    }
}
