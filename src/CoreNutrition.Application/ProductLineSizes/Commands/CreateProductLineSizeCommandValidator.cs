using FluentValidation;

using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.ProductLineSizeAggregate.Entities;

namespace CoreNutrition.Application.ProductLineSizes.Commands.CreateProductLineSize;

public class CreateProductLineSizeCommandValidator
  : AbstractValidator<CreateProductLineSizeCommand>
{
  public CreateProductLineSizeCommandValidator()
  {
    RuleFor(command => command.ProductLineId) // not null, follows Guid format
      .NotNull()
      .NotEmpty()
      .Length(36)
      .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$");
    RuleFor(command => command.RecommendedRetailPrice.Amount) // not null, greater than 0
      .NotNull()
      .GreaterThan(0);
    RuleFor(command => command.SizeVariant.Name)
      .MinimumLength(SizeVariant.Constraints.MinNameLength)
      .MaximumLength(SizeVariant.Constraints.MaxNameLength);
    RuleFor(command => command.SizeVariant.Units) // not null, greater than 0
      .NotNull()
      .GreaterThan(0);
    When(command => command.SizeVariant.SingleSizeVariantId is not null, () =>
    {
      RuleFor(command => command.SizeVariant.SingleSizeVariantId) // either null or follows Guid format
      .Length(36)
      .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$");
    });
    RuleFor(command => command.ProductLineId)
      .NotNull();
  }
}

// var singleSizeIdInput = command.SizeVariant.SingleSizeVariantId;

// if (singleSizeIdInput is null)
// {
//   SizeVariantId.CreateUnique();
// }
// else if (!Guid.TryParse(singleSizeIdInput, out var guid))
// {
//   SizeVariantId.Create(guid);
// }
// else
// {
//   return Errors.SizeVariant.InvalidSizeVariantId;
// }

// singleSizeIdInput = singleSizeIdInput is not null
//   ? SizeVariantId.Create(guid)
//   : SizeVariantId.CreateUnique()