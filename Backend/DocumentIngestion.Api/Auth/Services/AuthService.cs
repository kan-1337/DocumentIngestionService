using DocumentIngestion.Api.Auth.Models;

namespace DocumentIngestion.Api.Auth.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    // Fake users for demo purposes
    private readonly Dictionary<string, (string password, UserRole role)> _fakeUsers = new()
    {
        { "user", ("user123", UserRole.User) },
        { "admin", ("admin123", UserRole.Admin) }
    };

    public AuthService(ITokenService tokenService, ILogger<AuthService> logger)
    {
        _tokenService = tokenService;
        _logger = logger;
    }

    public Task<LoginResponse?> AuthenticateAsync(string username, string password)
    {
        var safeUsername = SanitizeForLogging(username);
        
        _logger.LogInformation("Authentication attempt for user: {Username}", safeUsername);

        if (!_fakeUsers.TryGetValue(username, out var userInfo))
        {
            _logger.LogWarning("User not found: {Username}", safeUsername);
            return Task.FromResult<LoginResponse?>(null);
        }

        if (userInfo.password != password)
        {
            _logger.LogWarning("Invalid password for user: {Username}", safeUsername);
            return Task.FromResult<LoginResponse?>(null);
        }

        var token = _tokenService.GenerateToken(username, userInfo.role);

        _logger.LogInformation("User authenticated successfully: {Username} with role {Role}", safeUsername, userInfo.role);

        return Task.FromResult<LoginResponse?>(new LoginResponse
        {
            Token = token,
            Username = username,
            Role = userInfo.role
        });
    }

    /// <summary>
    /// Sanitizes user input to prevent log injection attacks by removing newlines and control characters
    /// </summary>
    /// <param name="input">The user-provided input to sanitize</param>
    /// <returns>Sanitized string safe for logging</returns>
    private static string SanitizeForLogging(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        // Remove newlines and carriage returns to prevent log injection
        return input
            .Replace(Environment.NewLine, "")
            .Replace("\r", "")
            .Replace("\n", "")
            .Replace("\t", "");
    }
}
