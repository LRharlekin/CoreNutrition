using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
  public static class NutritionFacts
  {
    public static Error InvalidCalories => Error.Validation(
            code: "NutritionFacts.InvalidCalories",
            description: "Invalid calories amount: The amount of kcal (Calories) per 100 grams cannot be 0 or a negative number.");
    public static Error InvalidMacros => Error.Validation(
            code: "NutritionFacts.InvalidMacros",
            description: "Macro nutrients with invalid amounts: The amount of fat, saturated fat, carbohydrates, sugar, protein, and salte per 100 grams must be between 0 and 100.");
    public static Error InvalidOfWhichMacros => Error.Validation(
            code: "NutritionFacts.InvalidOfWhichMacros",
            description: "Nutrition Facts with invalid amounts: Sugars must be less than carbohydrates. Saturated fats must be less than fats.");
  }
}