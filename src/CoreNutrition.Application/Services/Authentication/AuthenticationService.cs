using CoreNutrition.Application.Common.Interfaces.Authentication;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.UserAggregate;

using ErrorOr;

namespace CoreNutrition.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
    _userRepository = userRepository;
  }
  public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
  {
    // 1. Validate the user doesn't exist yet
    if (_userRepository.GetUserByEmail(email) is not null)
    {
      // throw new Exception("User already exists");
      // return Errors.User.DuplicateEmail;
      return new[] { Errors.User.DuplicateEmail };
    }
    // 2. create user (with hashed password) & persist to DB
    User user = new User(
      firstName,
      lastName,
      email,
      password);

    _userRepository.Add(user);

    // 3. create JWT token 
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(
      user,
      token);
  }

  public ErrorOr<AuthenticationResult> Login(string email, string password)
  {
    // 1. Validate the user exists
    if (_userRepository.GetUserByEmail(email) is not User user)
    {
      return Errors.Authentication.InvalidCredentials;
    }

    // 2. Validate the password is correct
    if (user.Password != password)
    {
      return Errors.Authentication.InvalidCredentials;
    }

    // 3. Create JWT token
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(
      user,
      token);
  }
}