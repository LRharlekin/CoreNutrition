using CoreNutrition.Application.Common.Interfaces.Authentication;

namespace CoreNutrition.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;

  public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
  }
  public AuthenticationResult Register(string firstName, string lastName, string email, string password)
  {
    // validate if user already exists
    // create user (with hashed password)
    Guid userId = Guid.NewGuid();
    // create JWT token 
    var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

    return new AuthenticationResult(
      userId,
      firstName,
      lastName,
      email,
      token);
  }

  public AuthenticationResult Login(string email, string password)
  {
    // var id = Guid.NewGuid();
    // var token = _jwtTokenGenerator.GenerateToken(id, firstName, lastName);
    return new AuthenticationResult(
      Guid.NewGuid(),
      "firstName hard coded",
      "lastName hard coded",
      email,
      "token");
  }
}