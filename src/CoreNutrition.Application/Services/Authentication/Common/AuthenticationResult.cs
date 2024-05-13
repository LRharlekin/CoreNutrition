using CoreNutrition.Domain.UserAggregate;

namespace CoreNutrition.Application.Services.Authentication.Common;

public record AuthenticationResult(
  User User,
  string Token
);