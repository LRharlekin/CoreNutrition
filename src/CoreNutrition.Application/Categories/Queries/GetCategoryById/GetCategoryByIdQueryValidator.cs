using FluentValidation;

namespace CoreNutrition.Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryValidator
  : AbstractValidator<GetCategoryByIdQuery>
{
  public GetCategoryByIdQueryValidator()
  {
    RuleFor(query => query.Id)
      .NotNull()
      .NotEmpty();
    RuleFor(query => query.Id.ToString())
      .Length(36)
      .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$");
  }
}