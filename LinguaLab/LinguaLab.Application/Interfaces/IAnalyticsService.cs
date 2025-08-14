using LinguaLab.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Interfaces
{
    public interface IAnalyticsService
    {
        Task<IEnumerable<ActivityHeatmapDto>> GetUserActivityHeatmapAsync(Guid userId);
        Task<IEnumerable<ActivityHeatmapDto>> GetLearnedWordsChartDataAsync(Guid userId);
        Task<IEnumerable<CategoryAccuracyDto>> GetCategoryAccuracyAsync(Guid userId);
    }
}
