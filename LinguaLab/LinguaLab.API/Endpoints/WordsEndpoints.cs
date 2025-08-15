using LinguaLab.Application.DTOs;
using LinguaLab.Application.Interfaces;
using System.Security.Claims;

namespace LinguaLab.API.Endpoints
{
    public static class WordsEndpoints
    {
        public static IEndpointRouteBuilder MapWordsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/words")
                .WithTags("Words");

            group.RequireAuthorization();

            group.MapGet("/", async (IWordService wordService, HttpContext httpContext) =>
            {
                var userId = Guid.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var words = await wordService.GetWordsForUserAsync(userId);
                return Results.Ok(words);
            });

            group.MapPost("/", async (IWordService wordService, HttpContext httpContext, CreateWordDto createWordDto) =>
            {
                var userId = Guid.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var newWord = await wordService.CreateWordAsync(createWordDto, userId);
                return Results.Created($"/api/words/{newWord.Id}", newWord);
            });

            group.MapPut("/{id}", async (Guid id, UpdateWordDto updateDto, HttpContext httpContext, IWordService wordService) =>
            {
                var userId = Guid.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var updatedWord = await wordService.UpdateWordAsync(id, updateDto, userId);
                if (updatedWord == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(updatedWord);
            });

            group.MapDelete("/{id}", async (Guid id, HttpContext httpContext, IWordService wordService) =>
            {
                var useId = Guid.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var success = await wordService.DeleteWordAsync(id, useId);
                if (!success)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            });

            return app;
        }
    }
}
