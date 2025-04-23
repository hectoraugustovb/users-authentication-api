using Microsoft.AspNetCore.Identity;
using UsersAuthenticationApi.Models;

namespace UsersAuthenticationApi.Services
{
  public class PasswordService
  {
    private readonly IPasswordHasher<UserModel> passwordHasher;

    public PasswordService(IPasswordHasher<UserModel> passwordHasher)
    {
      this.passwordHasher = passwordHasher;
    }

    public string HashPassword(UserModel user, string plainPassword)
    {
      return passwordHasher.HashPassword(user, plainPassword);
    }

    public bool VerifyPassword(UserModel user, string hashedPassword, string plainPassword)
    {
      var result = passwordHasher.VerifyHashedPassword(user, hashedPassword, plainPassword);

      return result == PasswordVerificationResult.Success;
    }
  }
}
