using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
  public static class NutritionFacts
  {
    public static Error InvalidNutritionFacts => Error.Validation(
            code: "NutritionFacts.InvalidNutritionFacts",
            description: "Nutrition Facts with invalid amounts: All values except kcal (Calories) must be between 0 and 100. Sugars must be less than carbohydrates. Saturated fats must be less than fats.");
  }
}