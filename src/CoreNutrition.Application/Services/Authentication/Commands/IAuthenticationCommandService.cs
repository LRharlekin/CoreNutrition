using ErrorOr;

using CoreNutrition.Application.Services.Authentication.Common;

namespace CoreNutrition.Application.Services.Authentication.Commands;

public interface IAuthenticationCommandService
{
  ErrorOr<AuthenticationResult> Register(
    string firstName,
    string lastName,
    string email,
    string password);
}