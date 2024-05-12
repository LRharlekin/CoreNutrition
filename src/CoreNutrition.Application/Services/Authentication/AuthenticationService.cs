namespace CoreNutrition.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  public AuthenticationResult Register(string firstName, string lastName, string email, string password)
  {

    return new AuthenticationResult(
      Guid.NewGuid(),
      firstName,
      lastName,
      email,
      "token string");
  }

  public AuthenticationResult Login(string email, string password)
  {
    return new AuthenticationResult(
      Guid.NewGuid(),
      "firstName hard coded",
      "lastName hard coded",
      email,
      "token string");
  }
}