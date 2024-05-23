using FluentValidation;

// using CoreNutrition.Application.Common.Interfaces.Authentication;
using CoreNutrition.Domain.CategoryAggregate;
// using CoreNutrition.Domain.Common.Interfaces.Persistence;
// using CoreNutrition.Application.Authentication.Common;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator
  : AbstractValidator<UpdateCategoryCommand>
{
  public UpdateCategoryCommandValidator()
  {
    RuleFor(command => command.Id)
      .NotNull()
      .NotEmpty();
    Unless(command => command.Name == null, () =>
    {
      RuleFor(command => command.Name)
        .NotEmpty()
        .Length(Category.MinNameLength, Category.MaxNameLength);
    });
    Unless(command => command.Description == null, () =>
    {
      RuleFor(command => command.Description)
        .NotEmpty()
        .Length(Category.MinDescriptionLength, Category.MaxDescriptionLength);
    });
    Unless(command => command.CategoryImageUrl == null, () =>
    {
      RuleFor(command => command.CategoryImageUrl)
        .NotEmpty()
        .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _));
    });
  }
}