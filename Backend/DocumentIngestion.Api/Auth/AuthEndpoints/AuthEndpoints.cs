using DocumentIngestion.Api.Auth.Models;
using DocumentIngestion.Api.Auth.Services;

namespace DocumentIngestion.Api.Auth.AuthEndpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auth").WithTags("Authentication");

        // Login endpoint
        group.MapPost("/login", async (LoginRequest request, IAuthService authService) =>
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return Results.BadRequest(new { message = "Username and password are required." });
            }

            var response = await authService.AuthenticateAsync(request.Username, request.Password);

            if (response == null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(response);
        })
        .WithName("Login")
        .WithSummary("Authenticates a user and returns a JWT token")
        .WithDescription(@"Demo users:
            - Username: 'user', Password: 'user123' (User role)
            - Username: 'admin', Password: 'admin123' (Admin role)")
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .AllowAnonymous();
    }
}
