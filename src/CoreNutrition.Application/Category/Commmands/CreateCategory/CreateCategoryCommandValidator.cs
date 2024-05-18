using MediatR;
using ErrorOr;
using FluentValidation;

namespace CoreNutrition.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator
  : AbstractValidator<CreateCategoryCommand>
{
  public CreateCategoryCommandValidator()
  {
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.Description).NotEmpty();
    RuleFor(x => x.CategoryImageUrl)
      .NotEmpty();
  }
}