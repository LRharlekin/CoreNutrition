using CoreNutrition.Domain.UserAggregate;

namespace CoreNutrition.Application.Authentication.Common;

public record AuthenticationResult(
  User User,
  string Token
);