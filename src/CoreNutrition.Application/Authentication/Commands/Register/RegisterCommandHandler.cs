using MediatR;
using ErrorOr;

using CoreNutrition.Application.Common.Interfaces.Authentication;
using CoreNutrition.Domain.UserAggregate;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Application.Authentication.Common;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Application.Authentication.Commands.Register;

public class RegisterCommandHandler :
  IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public RegisterCommandHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<AuthenticationResult>> Handle(
    RegisterCommand command,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later

    // 1. Validate the user doesn't exist yet
    if (_userRepository.GetUserByEmail(command.Email) is not null)
    {
      // throw new Exception("User already exists");
      // return Errors.User.DuplicateEmail;
      return Errors.User.DuplicateEmail;
    }
    // 2. create user (with hashed password) & persist to DB
    User user = new User(
      command.FirstName,
      command.LastName,
      command.Email,
      command.Password);

    _userRepository.Add(user);

    // 3. create JWT token 
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(
      user,
      token);
  }
}


