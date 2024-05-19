using MediatR;
using ErrorOr;
using FluentValidation;

// using CoreNutrition.Application.Common.Interfaces.Authentication;
// using CoreNutrition.Domain.CategoryAggregate;
// using CoreNutrition.Domain.Common.Interfaces.Persistence;
// using CoreNutrition.Application.Authentication.Common;
// using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator
  : AbstractValidator<UpdateCategoryCommand>
{
  public UpdateCategoryCommandValidator()
  {
    // RuleFor(x => x.FirstName).NotEmpty();
    // RuleFor(x => x.LastName).NotEmpty();
    // RuleFor(x => x.Email)
    //   .NotEmpty()
    //   .EmailAddress();
    // RuleFor(x => x.Password).NotEmpty();
  }
}