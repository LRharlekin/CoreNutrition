using FluentValidation;

using CoreNutrition.Domain.CategoryAggregate;

namespace CoreNutrition.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator
  : AbstractValidator<CreateCategoryCommand>
{
  public CreateCategoryCommandValidator()
  {
    RuleFor(command => command.Name)
      .NotNull()
      .NotEmpty()
      .Length(Category.Constraints.MinNameLength, Category.Constraints.MaxNameLength);
    RuleFor(command => command.Description)
      .NotNull()
      .NotEmpty()
      .Length(Category.Constraints.MinDescriptionLength, Category.Constraints.MaxDescriptionLength);
    RuleFor(command => command.CategoryImageUrl)
      .NotNull()
      .NotEmpty()
      .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
      .WithMessage("The Category Image URL is not a valid URL.");
  }
}