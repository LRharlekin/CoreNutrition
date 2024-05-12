using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
// using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


using CoreNutrition.Application.Common.Interfaces.Authentication;

namespace CoreNutrition.Infrastructure.Authentication

{
  public class JwtTokenGenerator : IJwtTokenGenerator
  {
    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
      var signingCredentials = new SigningCredentials(
        new SymmetricSecurityKey(
          Encoding.UTF8.GetBytes("1-2-3-super-duper-mega-ultra-secret-key")),
        SecurityAlgorithms.HmacSha256
      );

      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
        new Claim(JwtRegisteredClaimNames.GivenName, firstName),
        new Claim(JwtRegisteredClaimNames.FamilyName, lastName)
        ,new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };

      var token = new JwtSecurityToken(
        issuer: "CoreNutrition",
        // audience TODO
        // expires: TODO,
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