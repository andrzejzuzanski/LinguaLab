using LinguaLab.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Interfaces
{
    public interface IAnalyticsRepository
    {
        Task<IEnumerable<ActivityHeatmapDto>> GetUserActivityByDayAsync(Guid userId);
        Task<IEnumerable<ActivityHeatmapDto>> GetLearnedWordsByDayAsync(Guid userId);
    }
}
