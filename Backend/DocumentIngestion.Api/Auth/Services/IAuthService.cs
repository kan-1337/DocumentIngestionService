using DocumentIngestion.Api.Auth.Models;

namespace DocumentIngestion.Api.Auth.Services;

public interface IAuthService
{
    Task<LoginResponse?> AuthenticateAsync(string username, string password);
}
