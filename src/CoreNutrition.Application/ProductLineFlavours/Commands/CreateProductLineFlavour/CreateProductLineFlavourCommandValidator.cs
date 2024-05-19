using FluentValidation;

using CoreNutrition.Domain.ProductLineFlavourAggregate;

namespace CoreNutrition.Application.ProductLineFlavours.Commands.CreateProductLineFlavour;

public class CreateProductLineFlavourCommandValidator
  : AbstractValidator<CreateProductLineFlavourCommand>
{
  public CreateProductLineFlavourCommandValidator()
  {
    RuleFor(x => x.Flavour)
      .NotEmpty()
      .MinimumLength(ProductLineFlavour.MinNameLength)
      .MaximumLength(ProductLineFlavour.MaxNameLength);
    RuleFor(x => x.FlavourImageUrl)
      .NotEmpty();
    RuleFor(x => x.ProductLineId)
      .NotNull();
  }
}