using Microsoft.AspNetCore.Identity;
using A5.Models;
namespace A5.Hasher
{
public  class BCryptPasswordHasher<TEmployee> : IPasswordHasher<TEmployee> where TEmployee : Employee
{
    public  string HashPassword(TEmployee employee, string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }
	
    public  PasswordVerificationResult VerifyHashedPassword(TEmployee employee, string hashedPassword, string providedPassword)
    {
        var isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

        if (isValid && BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, 12))
        {
            return PasswordVerificationResult.SuccessRehashNeeded;
        }

        return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }
}
}