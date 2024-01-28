using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;
using ToDoAppTemplate.Core.Infrastructure.Security.Models;

namespace ToDoAppTemplate.Core.Infrastructure.Security;

public sealed class PasswordHasher(IConfiguration configuration)
{
    private readonly SecurityConfiguration _configuration = configuration?.GetSection("Security")?.Get<SecurityConfiguration>() ?? throw new NullReferenceException("Security configuration is null");

    internal Argon2Config GetHasherConfiguration(string password)
    {
        var salt = new byte[16];
        RandomNumberGenerator.Create().GetBytes(salt);

        return new Argon2Config
        {
            Type = Argon2Type.DataIndependentAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = 10,
            MemoryCost = 32768,
            Lanes = 5,
            Threads = Environment.ProcessorCount,
            Password = Encoding.UTF8.GetBytes(password),
            Salt = salt,
            Secret = Encoding.UTF8.GetBytes(_configuration.ArgonSecret),
            HashLength = 20
        };
    }

    public string HashPassword(string password)
    {
        var config = GetHasherConfiguration(password);

        return Argon2.Hash(config);
    }
}
