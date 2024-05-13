using ErrorOr;

using CoreNutrition.Application.Common.Interfaces.Authentication;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.UserAggregate;
using CoreNutrition.Application.Services.Authentication.Common;

namespace CoreNutrition.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
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
}