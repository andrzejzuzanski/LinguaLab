using LinguaLab.Application.Interfaces;
using System.Security.Claims;

namespace LinguaLab.API.Endpoints
{
    public static class AnalyticsEndpoints
    {
        public static IEndpointRouteBuilder MapAnalyticsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/analytics")
                .WithTags("Analytics");

            group.RequireAuthorization();

            group.MapGet("/heatmap", async (HttpContext httpContext, IAnalyticsService analyticsService) =>
            {
                var userId = Guid.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var heatmapData = await analyticsService.GetUserActivityHeatmapAsync(userId);
                return Results.Ok(heatmapData);
            });

            group.MapGet("/learned-words-chart", async (HttpContext httpContext, IAnalyticsService analyticsService) =>
            {
                var userId = Guid.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var chartData = await analyticsService.GetLearnedWordsChartDataAsync(userId);
                return Results.Ok(chartData);
            });

            group.MapGet("/accuracy-by-category", async (HttpContext httpContext, IAnalyticsService analyticsService) =>
            {
                var userId = Guid.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var accuracyData = await analyticsService.GetCategoryAccuracyAsync(userId);
                return Results.Ok(accuracyData);
            });

            return app;
        }
    }
}
