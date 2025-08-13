using LinguaLab.Application.Interfaces;
using System.Security.Claims;

namespace LinguaLab.API.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/users")
                .WithTags("Users");

            group.MapGet("/me", async (HttpContext httpContext, IUserService userService) =>
            {
                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                if(userIdClaim == null)
                {
                    return Results.Unauthorized();
                }

                if (!Guid.TryParse(userIdClaim.Value, out var userId))
                {
                    return Results.BadRequest("Invalid user ID format.");
                }

                var userProfile = await userService.GetUserProfileAsync(userId);

                return userProfile != null ?Results.Ok(userProfile): Results.NotFound("User profile not found.");
            }).RequireAuthorization();

            return app;
        }
    }
}
