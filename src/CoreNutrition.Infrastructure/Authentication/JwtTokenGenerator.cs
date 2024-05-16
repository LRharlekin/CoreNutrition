using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using CoreNutrition.Domain.UserAggregate;
using CoreNutrition.Application.Common.Interfaces.Authentication;
using CoreNutrition.Domain.Common.Interfaces.Services;

namespace CoreNutrition.Infrastructure.Authentication

{
  public sealed class JwtTokenGenerator : IJwtTokenGenerator
  {
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    // TODO options pattern --> easier for unit tests
    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
      _jwtSettings = jwtOptions.Value;
      _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(User user)
    {
      var signingCredentials = new SigningCredentials(
        new SymmetricSecurityKey(
          Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
        SecurityAlgorithms.HmacSha256
      );

      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        // new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
        new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
        // claim for role "Admin"
        // new Claim(ClaimTypes.Role, "Admin"),

        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };

      Console.WriteLine($"issuer {_jwtSettings.Issuer}");
      Console.WriteLine($"audience {_jwtSettings.Audience}");

      var token = new JwtSecurityToken(
        expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
        issuer: _jwtSettings.Issuer,
        audience: _jwtSettings.Audience,
        claims: claims,
        signingCredentials: signingCredentials
      );

      // serialize JWT into compact string format
      /* 
      eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9. // header
      eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ. // payload
      SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c // signature
      */
      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}