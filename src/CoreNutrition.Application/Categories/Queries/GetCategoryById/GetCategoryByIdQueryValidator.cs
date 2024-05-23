using FluentValidation;

namespace CoreNutrition.Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryValidator
  : AbstractValidator<GetCategoryByIdQuery>
{
  public GetCategoryByIdQueryValidator()
  {
    RuleFor(query => query.Id)
      .NotNull()
      .NotEmpty()
      .Must(id => Guid.TryParse(id, out _));
  }
}