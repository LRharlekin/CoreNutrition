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

    // RuleFor(x => xId)
    //       .NotEmpty()
    //       .Length(36)
    //       .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$");
  }
}