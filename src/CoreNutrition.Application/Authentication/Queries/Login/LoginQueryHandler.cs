using MediatR;
using ErrorOr;

using CoreNutrition.Application.Common.Interfaces.Authentication;
using CoreNutrition.Domain.UserAggregate;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Application.Authentication.Common;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Application.Authentication.Queries.Login;

public class LoginQueryHandler :
  IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public LoginQueryHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<AuthenticationResult>> Handle(
    LoginQuery query,
    CancellationToken cancellationToken)
  {
    // 1. Validate the user exists
    if (_userRepository.GetUserByEmail(query.Email) is not User user)
    {
      return Errors.Authentication.InvalidCredentials;
    }

    // 2. Validate the password
    if (user.Password != query.Password)
    {
      return Errors.Authentication.InvalidCredentials;
    }

    // 3. create JWT token 
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(
      user,
      token);
  }
}


