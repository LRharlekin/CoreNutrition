using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class ProductLine
    {
        public static Error InvalidProductLineId => Error.Validation(
            code: "ProductLine.InvalidId",
            description: "Product Line ID is invalid");

        public static Error InvalidNutritionFacts => Error.Validation(
            code: "ProductLine.InvalidNutritionFacts",
            description: "Nutrition Facts are invalid: Values must be between 0 and 100. Sugars must be less than carbohydrates. Saturated fats must be less than fats.");

        public static Error NotFound => Error.NotFound(
            code: "ProductLine.NotFound",
            description: "Product Line with given ID does not exist");
    }
}