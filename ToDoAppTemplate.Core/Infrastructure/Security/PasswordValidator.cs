using Isopoh.Cryptography.Argon2;

namespace ToDoAppTemplate.Core.Infrastructure.Security;

public sealed class PasswordValidator
{
    private readonly PasswordHasher _passwordHasher;

    public PasswordValidator(PasswordHasher passwordHasher) => _passwordHasher = passwordHasher;

    /// <summary>
    /// Validates the input password against a saved hashed password
    /// </summary>
    /// <param name="password">The plaintext input password</param>
    /// <param name="hashedPassword">The hashed stored password</param>
    /// <returns>Returns true if validation is successful, otherwise returns false</returns>
    public bool ValidatePassword(string password, string hashedPassword)
    {
        var config = _passwordHasher.GetHasherConfiguration(password);

        return Argon2.Verify(hashedPassword, config);
    }
}
