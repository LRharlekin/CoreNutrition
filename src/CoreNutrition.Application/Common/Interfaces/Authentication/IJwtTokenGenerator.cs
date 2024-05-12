using CoreNutrition.Domain.UserAggregate;

namespace CoreNutrition.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
  string GenerateToken(User user);
}
