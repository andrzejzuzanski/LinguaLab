using LinguaLab.Application.DTOs;
using LinguaLab.Application.Interfaces;

namespace LinguaLab.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth")
                .WithTags("Authentication");

            group.MapPost("/register", async (RegisterUserDto registerDto, IAuthService authService) =>
            {
                var result = await authService.RegisterUserAsync(registerDto);

                if (!result)
                {
                    return Results.BadRequest("User with this email already exists.");
                }

                return Results.StatusCode(StatusCodes.Status201Created);
            });

            group.MapPost("/login", async (LoginUserDto loginDto, IAuthService authService) =>
            {
                var loginResponse = await authService.LoginUserAsync(loginDto);

                if (loginResponse == null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(loginResponse);
            });

            return app;
        }
    }
}