using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using CoreNutrition.Domain.UserAggregate;
using CoreNutrition.Application.Common.Interfaces.Authentication;
using CoreNutrition.Domain.Common.Interfaces.Services;

namespace CoreNutrition.Infrastructure.Security.TokenGenerator;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
  private readonly JwtSettings _jwtSettings;
  private readonly IDateTimeProvider _dateTimeProvider;

  public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
  {
    _dateTimeProvider = dateTimeProvider;
    _jwtSettings = jwtOptions.Value;
  }

  public string GenerateToken(User user)
  {
    Console.WriteLine("JwtTokenGenerator.GenerateToken");
    Console.WriteLine($"_jwtSettings.Issuer {_jwtSettings.Issuer}");
    Console.WriteLine($"_jwtSettings.Audience {_jwtSettings.Audience}");

    var signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
      SecurityAlgorithms.HmacSha256
    );

    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString() ?? ""),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
        new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
        // claims for roles
        new Claim(ClaimTypes.Role, "Admin"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };

    var token = new JwtSecurityToken(
      expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
      issuer: _jwtSettings.Issuer,
      audience: _jwtSettings.Audience,
      claims: claims,
      signingCredentials: signingCredentials
    );

    Console.WriteLine($"raw token before handler:");
    Console.WriteLine($"{token}");

    // serialize JWT into compact string format
    /* 
    eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9. // header
    eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ. // payload
    SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c // signature
    */
    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}