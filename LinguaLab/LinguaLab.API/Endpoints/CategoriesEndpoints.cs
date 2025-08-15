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

            group.MapPut("/{id}", async (Guid id, UpdateCategoryDto updateDto, ICategoryService categoryService) =>
            {
                var updatedCategory = await categoryService.UpdateCategoryAsync(id, updateDto);

                if (updatedCategory == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(updatedCategory);

            }).RequireAuthorization();

            group.MapDelete("/{id}", async (Guid id, ICategoryService categoryService) =>
            {
                try
                {
                    var success = await categoryService.DeleteCategoryAsync(id);
                    if (!success)
                    {
                        return Results.NotFound();
                    }
                    return Results.NoContent();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new {message = ex.Message});
                }
            }).RequireAuthorization();

            return app;
        }
    }
}
