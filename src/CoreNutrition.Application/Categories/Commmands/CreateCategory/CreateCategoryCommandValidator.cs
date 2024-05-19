using FluentValidation;

using CoreNutrition.Domain.CategoryAggregate;

namespace CoreNutrition.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator
  : AbstractValidator<CreateCategoryCommand>
{
  public CreateCategoryCommandValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .MinimumLength(Category.MinNameLength)
      .MaximumLength(Category.MaxNameLength);
    RuleFor(x => x.Description)
      .NotEmpty()
      .MinimumLength(Category.MinDescriptionLength)
      .MaximumLength(Category.MaxDescriptionLength);
    RuleFor(x => x.CategoryImageUrl)
      .NotEmpty();
  }
}