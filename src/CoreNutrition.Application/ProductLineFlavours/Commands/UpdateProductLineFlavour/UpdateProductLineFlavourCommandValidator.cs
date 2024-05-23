using FluentValidation;

using CoreNutrition.Domain.ProductLineFlavourAggregate;

namespace CoreNutrition.Application.ProductLineFlavours.Commands.UpdateProductLineFlavour;

public class UpdateProductLineFlavourCommandValidator
  : AbstractValidator<UpdateProductLineFlavourCommand>
{
  public UpdateProductLineFlavourCommandValidator()
  {
    RuleFor(command => command.Flavour)
      .NotNull()
      .NotEmpty()
      .Length(ProductLineFlavour.MinNameLength, ProductLineFlavour.MaxNameLength);
    RuleFor(command => command.FlavourImageUrl)
      .NotNull()
      .NotEmpty()
      .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _));
    RuleFor(command => command.ProductLineId)
      .NotNull()
      .NotEmpty()
      .Must(id => Guid.TryParse(id, out _));
  }
}