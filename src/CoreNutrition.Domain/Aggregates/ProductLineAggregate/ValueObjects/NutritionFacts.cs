using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

public sealed class NutritionFacts : ValueObject
{
  // invariant constants:
  public static class Constraints
  {
    public const double MinPer100Grams = 0;
    public const double MaxPer100Grams = 100;
  }
  

  public double CaloriesPer100Grams { get; private set; }
  public double FatPer100Grams { get; private set; }
  public double SaturatedFatPer100Grams { get; private set; }
  public double CarbohydratesPer100Grams { get; private set; }
  public double SugarPer100Grams { get; private set; }
  public double ProteinPer100Grams { get; private set; }
  public double SaltPer100Grams { get; private set; }

  private NutritionFacts(
    double caloriesPer100Grams,
    double fatPer100Grams,
    double saturatedFatPer100Grams,
    double carbohydratesPer100Grams,
    double sugarPer100Grams,
    double proteinPer100Grams,
    double saltPer100Grams)
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
    double caloriesPer100Grams,
    double fatPer100Grams,
    double saturatedFatPer100Grams,
    double carbohydratesPer100Grams,
    double sugarPer100Grams,
    double proteinPer100Grams,
    double saltPer100Grams)
  {
    var nutritionFacts = new NutritionFacts(
      caloriesPer100Grams,
      fatPer100Grams,
      saturatedFatPer100Grams,
      carbohydratesPer100Grams,
      sugarPer100Grams,
      proteinPer100Grams,
      saltPer100Grams);

    var errors = nutritionFacts.EnforceInvariants();

    if (errors.Count > 0)
    {
      return errors;
    }

    return nutritionFacts;
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

  private List<Error> EnforceInvariants()
  {
    var errors = new List<Error>();

    if (this.CaloriesPer100Grams <= Constraints.MinPer100Grams)
    {
      errors.Add(Errors.NutritionFacts.InvalidCalories);
    }

    if (!IsValidMacro(this.FatPer100Grams) ||
      !IsValidMacro(this.CarbohydratesPer100Grams) ||
      !IsValidMacro(this.ProteinPer100Grams) ||
      !IsValidMacro(this.SaltPer100Grams))
    {
      errors.Add(Errors.NutritionFacts.InvalidMacros);
    }

    if (!IsValidOfWhichMacro(this.SaturatedFatPer100Grams, this.FatPer100Grams) ||
      !IsValidOfWhichMacro(this.SugarPer100Grams, this.CarbohydratesPer100Grams))
    {
      errors.Add(Errors.NutritionFacts.InvalidOfWhichMacros);
    }

    static bool IsValidMacro(double value)
    {
      return value >= Constraints.MinPer100Grams && value <= Constraints.MaxPer100Grams;
    }

    static bool IsValidOfWhichMacro(double value, double macroValue)
    {
      return value >= Constraints.MinPer100Grams && value <= macroValue;
    }

    return errors;
  }
}