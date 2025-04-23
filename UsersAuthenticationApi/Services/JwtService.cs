using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UsersAuthenticationApi.Services
{
  public class JwtService
  {
    private readonly string secretKey;
    private readonly string issuer;
    private readonly string audience;

    public JwtService(IConfiguration configuration)
    {
      secretKey = configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("SecretKey");
      issuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Issuer");
      audience = configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Audience");
    }

    public string GenerateToken(string Email)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var claims = new[]
      {
        new Claim(ClaimTypes.Email, Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };

      var tokenDescriptor = new JwtSecurityToken(
        issuer: issuer,
        audience: audience,
        claims: claims,
        expires: DateTime.UtcNow.AddDays(1),
        signingCredentials: credentials
      );

      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.WriteToken(tokenDescriptor);

      return token;
    }
  }
}
