using DocumentIngestion.Api.Auth.Models;

namespace DocumentIngestion.Api.Auth.Services;

public interface ITokenService
{
    string GenerateToken(string username, UserRole role);
}
