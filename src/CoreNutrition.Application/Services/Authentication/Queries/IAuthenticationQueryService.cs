using ErrorOr;

using CoreNutrition.Application.Services.Authentication.Common;

namespace CoreNutrition.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
  ErrorOr<AuthenticationResult> Login(
    string email,
    string password);
}