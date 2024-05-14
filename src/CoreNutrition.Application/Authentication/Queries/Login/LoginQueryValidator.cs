using MediatR;
using ErrorOr;
using FluentValidation;

namespace CoreNutrition.Application.Authentication.Queries.Login;

public class LoginQueryValidator
  : AbstractValidator<LoginQuery>
{
  public LoginQueryValidator()
  {
    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress();
    RuleFor(x => x.Password).NotEmpty();
  }
}