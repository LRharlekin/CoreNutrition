using FluentValidation;

using CoreNutrition.Domain.ProductLineFlavourAggregate;

namespace CoreNutrition.Application.ProductLineFlavours.Commands.UpdateProductLineFlavour;

public class UpdateProductLineFlavourCommandValidator
  : AbstractValidator<UpdateProductLineFlavourCommand>
{
  public UpdateProductLineFlavourCommandValidator()
  {
    RuleFor(command => command.Id)
      .NotNull()
      .NotEmpty();
    Unless(command => command.Flavour == null, () =>
      {
        RuleFor(command => command.Flavour)
          .NotEmpty()
          .Length(ProductLineFlavour.Constraints.MinNameLength, ProductLineFlavour.Constraints.MaxNameLength);
      });
    Unless(command => command.FlavourImageUrl == null, () =>
    {
      RuleFor(command => command.FlavourImageUrl)
        .NotEmpty()
        .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
        .WithMessage("The Flavour Image URL is not a valid URL.");
    });
    Unless(command => command.ProductLineId == null, () =>
    {

      RuleFor(command => command.ProductLineId)
        .NotEmpty()
        .Must(id => Guid.TryParse(id, out _));
    });
  }
}