using FluentValidation;

namespace CoreNutrition.Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQuerydValidator
  : AbstractValidator<GetCategoryByIdQuery>
{
  public GetCategoryByIdQuerydValidator()
  {
    RuleFor(x => x.Id.Value.ToString())
      .Length(36)
      .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$");
  }
}