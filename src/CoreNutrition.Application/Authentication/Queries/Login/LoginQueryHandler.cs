using MediatR;
using ErrorOr;

using CoreNutrition.Domain.UserAggregate;
using CoreNutrition.Domain.UserAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.Services;
using CoreNutrition.Application.Authentication.Common;
using CoreNutrition.Application.Common.Interfaces.Cryptography;
using CoreNutrition.Application.Common.Interfaces.Authentication;


namespace CoreNutrition.Application.Authentication.Queries.Login;

internal sealed class LoginQueryHandler :
  IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IPasswordHashChecker _passwordHashChecker;
  private readonly IUserRepository _userRepository;

  public LoginQueryHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHashChecker passwordHashChecker,
    IUserRepository userRepository)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
    _passwordHashChecker = passwordHashChecker;
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<AuthenticationResult>> Handle(
    LoginQuery query,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later

    // 1. create value objets from pw and email
    var emailResult = Email.CreateNew(query.Email);
    var passwordResult = Password.CreateNew(query.Password);

    List<Error> errors = [];

    if (emailResult.IsError)
    {
      errors.Add(emailResult.FirstError);
    }
    if (passwordResult.IsError)
    {
      errors.Add(passwordResult.FirstError);
    }

    if (errors.Count > 0)
    {
      return errors;
    }

    // 2. Validate the user exists
    // if (_userRepository.GetUserByEmail(query.Email) is not User user)
    if (_userRepository.GetUserByEmail(emailResult.Value) is not User user)
    {
      return Errors.Authentication.InvalidCredentials;
    }

    // 3. Validate the password
    // if (user.Password != query.Password)
    // {
    //   return Errors.Authentication.InvalidCredentials;
    // }
    bool passwordValid = user.VerifyPasswordHash(query.Password, _passwordHashChecker);

    if (!passwordValid)
    {
      return Errors.Authentication.InvalidCredentials;
    }

    // 4. genearte and return JWT token 
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(
      user,
      token);
  }
}


