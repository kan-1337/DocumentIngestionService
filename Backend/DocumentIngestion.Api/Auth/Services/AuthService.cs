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
        _logger.LogInformation("Authentication attempt for user: {Username}", username);

        if (!_fakeUsers.TryGetValue(username, out var userInfo))
        {
            _logger.LogWarning("User not found: {Username}", username);
            return Task.FromResult<LoginResponse?>(null);
        }

        if (userInfo.password != password)
        {
            _logger.LogWarning("Invalid password for user: {Username}", username);
            return Task.FromResult<LoginResponse?>(null);
        }

        var token = _tokenService.GenerateToken(username, userInfo.role);

        _logger.LogInformation("User authenticated successfully: {Username} with role {Role}", username, userInfo.role);

        return Task.FromResult<LoginResponse?>(new LoginResponse
        {
            Token = token,
            Username = username,
            Role = userInfo.role
        });
    }
}
