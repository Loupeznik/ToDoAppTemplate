using System.Security.Cryptography;
using ToDoAppTemplate.Domain.Common;

namespace ToDoAppTemplate.Domain.Users;

public sealed class PasswordResetCode : BaseEntity
{
    private const int CodeLength = 8;
    private const string AllowedCodeCharacters = "0123456789";

    public int UserId { get; set; }

    public User? User { get; set; }

    public string Code { get; set; } = string.Empty;

    public DateTime ExpirationDate { get; set; }

    public bool IsUsed { get; set; }

    public static string GenerateResetCode()
    {
        var result = new char[CodeLength];
        for (var i = 0; i < CodeLength; i++)
        {
            result[i] = AllowedCodeCharacters[RandomNumberGenerator.GetInt32(0, AllowedCodeCharacters.Length)];
        }

        return new string(result);
    }
}
