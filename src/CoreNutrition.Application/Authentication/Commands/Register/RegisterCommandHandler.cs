using MediatR;
using ErrorOr;

using CoreNutrition.Domain.UserAggregate;
using CoreNutrition.Domain.UserAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Application.Authentication.Common;
using CoreNutrition.Application.Common.Interfaces.Cryptography;
using CoreNutrition.Application.Common.Interfaces.Authentication;

namespace CoreNutrition.Application.Authentication.Commands.Register;

internal sealed class RegisterCommandHandler :
  IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IUserRepository _userRepository;

  public RegisterCommandHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher,
    IUserRepository userRepository)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
    _passwordHasher = passwordHasher;
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<AuthenticationResult>> Handle(
    RegisterCommand command,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later

    // 1. Create value objects for Email
    var firstNameResult = FirstName.CreateNew(command.FirstName);
    var lastNameResult = LastName.CreateNew(command.LastName);
    var emailResult = Email.CreateNew(command.Email);
    var passwordResult = Password.CreateNew(command.Password);

    List<Error> errors = [];

    if (firstNameResult.IsError)
    {
      errors.Add(firstNameResult.FirstError);
    }
    if (lastNameResult.IsError)
    {
      errors.Add(lastNameResult.FirstError);
    }
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

    // 2. validate the user doesn't exist yet
    // if (_userRepository.GetUserByEmailAsync(emailResult.Value) is not null)
    if (_userRepository.GetUserByEmail(emailResult.Value) is not null)
    // if (_userRepository.GetUserByEmail(command.Email) is not null)
    {
      return Errors.User.DuplicateEmail;
    }

    // 3.  hash password
    string passwordHash = _passwordHasher.HashPassword(passwordResult.Value);

    // 4. create user (with hashed password) 
    // var user = new User(_jwtTokenGenerator
    // firstNameResult.Value,
    // lastNameResult.Value,
    // emailResult.Value,
    // PasswordHasher);

    var user = User.Create(
      firstNameResult.Value,
      lastNameResult.Value,
      emailResult.Value,
      passwordHash);

    // 5. persist to DB
    // _userRepository.Insert(user);
    _userRepository.Add(user);

    // await _unitOfWork.SaveChangesAsync(cancellationToken);

    // 6. generate and return JWT token 
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(
      user,
      token);
  }
}


