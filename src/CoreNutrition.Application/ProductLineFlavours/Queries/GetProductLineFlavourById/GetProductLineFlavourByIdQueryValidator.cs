using FluentValidation;

namespace CoreNutrition.Application.ProductLineFlavours.Queries.GetProductLineFlavourById;

public class GetProductLineFlavourByIdQueryValidator
  : AbstractValidator<GetProductLineFlavourByIdQuery>
{
  public GetProductLineFlavourByIdQueryValidator()
  {
    RuleFor(query => query.Id)
      .NotNull()
      .NotEmpty();
    RuleFor(query => query.Id.ToString())
      .Length(36)
      .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$");
  }
}