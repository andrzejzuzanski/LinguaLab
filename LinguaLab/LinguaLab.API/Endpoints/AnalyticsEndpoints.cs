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

            return app;
        }
    }
}
