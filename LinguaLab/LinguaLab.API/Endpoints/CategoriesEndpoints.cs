using LinguaLab.Application.DTOs;
using LinguaLab.Application.Interfaces;

namespace LinguaLab.API.Endpoints
{
    public static class CategoriesEndpoints
    {
        public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/categories")
                .WithTags("Categories");

            group.MapGet("/", async (ICategoryService categoryService) =>
            {
                var categories = await categoryService.GetAllCategoriesAsync();
                return Results.Ok(categories);
            });

            group.MapPost("/", async (ICategoryService categoryService, CreateCategoryDto createCategoryDto) =>
            {
                var newCategory = await categoryService.CreateCategoryAsync(createCategoryDto);
                return Results.Created($"/api/categories/{newCategory.Id}", newCategory);
            }).RequireAuthorization();

            return app;
        }
    }
}
