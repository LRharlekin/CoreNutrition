using FluentValidation;

using CoreNutrition.Domain.ProductLineFlavourAggregate;

namespace CoreNutrition.Application.ProductLineFlavours.Commands.CreateProductLineFlavour;

public class CreateProductLineFlavourCommandValidator
  : AbstractValidator<CreateProductLineFlavourCommand>
{
  public CreateProductLineFlavourCommandValidator()
  {
    RuleFor(command => command.Flavour)
      .NotNull()
      .NotEmpty()
      .Length(ProductLineFlavour.MinNameLength, ProductLineFlavour.MaxNameLength);
    RuleFor(command => command.ProductLineId)
      .NotNull()
      .NotEmpty()
      .Must(id => Guid.TryParse(id, out _));
    RuleFor(command => command.FlavourImageUrl)
      .NotNull()
      .NotEmpty()
      .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _));
  }
}