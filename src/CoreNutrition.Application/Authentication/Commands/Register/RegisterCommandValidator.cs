using MediatR;
using ErrorOr;
using FluentValidation;

// using CoreNutrition.Application.Common.Interfaces.Authentication;
// using CoreNutrition.Domain.UserAggregate;
// using CoreNutrition.Domain.Common.Interfaces.Persistence;
// using CoreNutrition.Application.Authentication.Common;
// using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Application.Authentication.Commands.Register;

public class RegisterCommandValidator
  : AbstractValidator<RegisterCommand>
{
  public RegisterCommandValidator()
  {
    RuleFor(x => x.FirstName).NotEmpty();
    RuleFor(x => x.LastName).NotEmpty();
    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress();
    RuleFor(x => x.Password).NotEmpty();
  }
}