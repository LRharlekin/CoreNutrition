using FluentValidation;

using CoreNutrition.Domain.ProductLineAggregate;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Application.ProductLines.Commands.CreateProductLine;

public class CreateProductLineCommandValidator
  : AbstractValidator<CreateProductLineCommand>
{
  public CreateProductLineCommandValidator()
  {
    RuleFor(command => command.Name)
      .NotNull()
      .NotEmpty()
      .Length(ProductLine.Constraints.MinNameLength, ProductLine.Constraints.MaxNameLength);
    RuleFor(command => command.CategoryId)
      .NotNull()
      .NotEmpty()
      .Must(id => Guid.TryParse(id, out _));
    RuleFor(command => command.IsPublished)
      .NotNull();
    RuleFor(command => command.ProductLineInfo)
      .NotNull();
    RuleFor(command => command.ProductLineInfo.DescriptionShort)
      .NotNull()
      .NotEmpty()
      .Length(ProductLineInfo.MinDescriptionShortLength, ProductLineInfo.MaxDescriptionShortLength);
    RuleFor(command => command.ProductLineInfo.DescriptionLong)
      .NotNull()
      .NotEmpty()
      .Length(ProductLineInfo.MinDescriptionLongLength, ProductLineInfo.MaxDescriptionLongLength);
    RuleFor(command => command.ProductLineInfo.SuggestedUse)
      .NotNull()
      .NotEmpty()
      .Length(ProductLineInfo.MinSuggestedUseLength, ProductLineInfo.MaxSuggestedUseLength);
    RuleForEach(command => new List<string> {
      command.ProductLineInfo.Benefit1,
      command.ProductLineInfo.Benefit2,
      command.ProductLineInfo.Benefit3})
      .NotNull()
      .NotEmpty()
      .MaximumLength(ProductLineInfo.MaxBenefitLength)
      .OverridePropertyName("Benefit");
    RuleFor(command => command.ProductLineInfo.IsMuscleGain)
      .NotNull();
    RuleFor(command => command.ProductLineInfo.IsWeightLoss)
      .NotNull();
    RuleFor(command => command.ProductLineInfo.IsHealthWellness)
      .NotNull();
    RuleFor(command => command.NutritionFacts)
      .NotNull();
    RuleFor(command => command.NutritionFacts.CaloriesPer100Grams)
      .NotNull()
      .GreaterThan(0);
    RuleForEach(command => new List<double> {
      command.NutritionFacts.FatPer100Grams,
      command.NutritionFacts.SaturatedFatPer100Grams,
      command.NutritionFacts.CarbohydratesPer100Grams,
      command.NutritionFacts.SugarPer100Grams,
      command.NutritionFacts.ProteinPer100Grams,
      command.NutritionFacts.SaltPer100Grams,
    })
      .NotNull()
      .GreaterThanOrEqualTo(0)
      .LessThanOrEqualTo(100)
      .OverridePropertyName("NutritionFacts");
  }
}