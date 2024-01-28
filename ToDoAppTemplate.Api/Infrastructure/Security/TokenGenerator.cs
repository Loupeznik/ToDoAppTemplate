using System.Security.Claims;
using ToDoAppTemplate.Core.Auth.Models;
using FastEndpoints.Security;

namespace ToDoAppTemplate.Api.Infrastructure.Security;

public sealed class TokenGenerator
{
    private readonly AuthConfiguration _configuration;

    public TokenGenerator(AuthConfiguration configuration) => _configuration = configuration;

    public string GenerateToken(AuthResult result, string login)
    {
        return JWTBearer.CreateToken(signingKey: _configuration.SigningKey, issuer: _configuration.Issuer,
            expireAt: DateTime.Now.AddMinutes(_configuration.TokenExpirationInMinutes), privileges:
            x =>
            {
                x["UserID"] = result.UserId.GetValueOrDefault().ToString();
                x.Claims.Add(new Claim(ClaimTypes.Name, login));
            });
    }
}
