using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

public sealed class NutritionFacts : ValueObject
{
  public decimal CaloriesPer100Grams { get; private set; }
  public decimal FatPer100Grams { get; private set; }
  public decimal SaturatedFatPer100Grams { get; private set; }
  public decimal CarbohydratesPer100Grams { get; private set; }
  public decimal SugarPer100Grams { get; private set; }
  public decimal ProteinPer100Grams { get; private set; }
  public decimal SaltPer100Grams { get; private set; }

  private NutritionFacts(
    decimal caloriesPer100Grams,
    decimal fatPer100Grams,
    decimal saturatedFatPer100Grams,
    decimal carbohydratesPer100Grams,
    decimal sugarPer100Grams,
    decimal proteinPer100Grams,
    decimal saltPer100Grams)
  {
    CaloriesPer100Grams = caloriesPer100Grams;
    FatPer100Grams = fatPer100Grams;
    SaturatedFatPer100Grams = saturatedFatPer100Grams;
    CarbohydratesPer100Grams = carbohydratesPer100Grams;
    SugarPer100Grams = sugarPer100Grams;
    ProteinPer100Grams = proteinPer100Grams;
    SaltPer100Grams = saltPer100Grams;
  }

  public static ErrorOr<NutritionFacts> CreateNew(
    decimal caloriesPer100Grams,
    decimal fatPer100Grams,
    decimal saturatedFatPer100Grams,
    decimal carbohydratesPer100Grams,
    decimal sugarPer100Grams,
    decimal proteinPer100Grams,
    decimal saltPer100Grams)
  {
    // Validate macros
    if (!IsValidMacro(fatPer100Grams) ||
      !IsValidMacro(carbohydratesPer100Grams) ||
      !IsValidMacro(proteinPer100Grams) ||
      !IsValidMacro(saltPer100Grams))
      return Errors.NutritionFacts.InvalidNutritionFacts;

    // Validate derived nutrients
    if (!IsValidOfWhichMacro(saturatedFatPer100Grams, fatPer100Grams) ||
      !IsValidOfWhichMacro(sugarPer100Grams, carbohydratesPer100Grams))
      return Errors.NutritionFacts.InvalidNutritionFacts;

    return new NutritionFacts(
      caloriesPer100Grams,
      fatPer100Grams,
      saturatedFatPer100Grams,
      carbohydratesPer100Grams,
      sugarPer100Grams,
      proteinPer100Grams,
      saltPer100Grams);
  }

  private static bool IsValidMacro(decimal value)
  {
    return value >= 0 && value <= 100;
  }

  private static bool IsValidOfWhichMacro(decimal value, decimal macroValue)
  {
    return value >= 0 && value <= macroValue;
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return CaloriesPer100Grams;
    yield return FatPer100Grams;
    yield return SaturatedFatPer100Grams;
    yield return CarbohydratesPer100Grams;
    yield return SugarPer100Grams;
    yield return ProteinPer100Grams;
    yield return SaltPer100Grams;
  }

#pragma warning disable CS8618
  private NutritionFacts()
  {
  }
#pragma warning restore CS8618
}