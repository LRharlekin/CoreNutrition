using CoreNutrition.Domain.UserAggregate;

namespace CoreNutrition.Application.Services.Authentication;

public record AuthenticationResult(
  User User,
  string Token
);