using LinguaLab.Application.DTOs;
using LinguaLab.Application.Interfaces;
using System.Security.Claims;

namespace LinguaLab.API.Endpoints
{
    public static class LearningEndpoints
    {
        public static IEndpointRouteBuilder MapLearningEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/learning").WithTags("Learning");

            group.RequireAuthorization();

            group.MapGet("/session", async (HttpContext httpContext, ILearningService learningService) =>
            {
                var userId = Guid.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var words = await learningService.GetWordsForSessionAsync(userId);

                return Results.Ok(words);
            });

            group.MapPost("/answer", async (SubmitAnswerDto answerDto, HttpContext httpContext, ILearningService learningService) =>
            {
                var userId = Guid.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                try
                {
                    await learningService.ProcessAnswerAsync(userId, answerDto);
                    return Results.Ok();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            });

            return app;
        }
    }
}
