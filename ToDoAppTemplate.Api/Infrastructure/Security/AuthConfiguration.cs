namespace ToDoAppTemplate.Api.Infrastructure.Security;

public sealed class AuthConfiguration
{
    public AuthConfiguration(string signingKey, string issuer, int tokenExpirationInMinutes)
    {
        SigningKey = signingKey;
        Issuer = issuer;
        TokenExpirationInMinutes = tokenExpirationInMinutes;
    }

    public string SigningKey { get; init; }
    
    public string Issuer { get; init; }
    
    public int TokenExpirationInMinutes { get; init; }
}
