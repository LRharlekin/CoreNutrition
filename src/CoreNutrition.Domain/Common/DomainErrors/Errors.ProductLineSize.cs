using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class ProductLineSize
    {
        public static Error InvalidRecommendedRetailPrice => Error.Validation(
            code: "ProductLineSize.InvalidRRP",
            description: "The Recommended Retail Price must be greater than 0."
        );

        public static Error InvalidProductLineSizeId => Error.Validation(
            code: "ProductLineSize.InvalidId",
            description: "Product Line Size ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "ProductLineSize.NotFound",
            description: "Product Line Size with given ID does not exist");
    }
}