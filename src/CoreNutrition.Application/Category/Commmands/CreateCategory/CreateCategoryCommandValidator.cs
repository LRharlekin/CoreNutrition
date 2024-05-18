using MediatR;
using ErrorOr;
using FluentValidation;

namespace CoreNutrition.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator
  : AbstractValidator<CreateCategoryCommand>
{
  public CreateCategoryCommandValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .MaximumLength(50);
    RuleFor(x => x.Description)
    .NotEmpty()
    .MinimumLength(20)
    .MaximumLength(800);
    RuleFor(x => x.CategoryImageUrl)
      .NotEmpty();
  }
}