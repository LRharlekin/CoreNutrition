using CoreNutrition.Application.Common.Interfaces.Authentication;

namespace CoreNutrition.Infrastructure.Authentication

{
  public class JwtTokenGenerator : IJwtTokenGenerator
  {
    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
      return "32140987öklsdfjg";
    }
  }
}