using CoreNutrition.Application.Common.Interfaces.Authentication;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.UserAggregate;

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
  public AuthenticationResult Register(string firstName, string lastName, string email, string password)
  {
    // 1. Validate the user doesn't exist yet
    if (_userRepository.GetUserByEmail(email) is not null)
    {
      throw new Exception("User already exists");
    }
    // 2. create user (with hashed password) & persist to DB
    var user = new User
    {
      FirstName = firstName,
      LastName = lastName,
      Email = email,
      Password = password
    };

    _userRepository.Add(user);

    // 3. create JWT token 

    // Guid userId = Guid.NewGuid();

    var token = _jwtTokenGenerator.GenerateToken(
      user.Id,
      firstName,
      lastName
    );

    return new AuthenticationResult(
      user.Id,
      firstName,
      lastName,
      email,
      token);
  }

  public AuthenticationResult Login(string email, string password)
  {
    // 1. Validate the user exists
    var user = _userRepository.GetUserByEmail(email);

    if (user is null)
    {
      throw new Exception("Invalid credentials");
    }

    // 2. Validate the password is correct
    if (user.Password != password)
    {
      throw new Exception("Invalid credentials");
    }

    // 3. Create JWT token
    var token = _jwtTokenGenerator.GenerateToken(
      user.Id,
      user.FirstName,
      user.LastName
    );

    return new AuthenticationResult(
      user.Id,
      user.FirstName,
      user.LastName,
      email,
      token);
  }
}