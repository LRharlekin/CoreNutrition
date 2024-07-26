using ErrorOr;

using CoreNutrition.Domain.ProductLineAggregate;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class ProductLine
    {
        public static Error InvalidNameLength => Error.Validation(
            code: "ProductLine.InvalidNameLength",
            description: $"The Name for a Product Line must be between {ProductLineAggregate.ProductLine.Constraints.MinNameLength} and {ProductLineAggregate.ProductLine.Constraints.MaxNameLength} characters long.");
        public static Error InvalidProductLineId => Error.Validation(
            code: "ProductLine.InvalidId",
            description: "Product Line ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "ProductLine.NotFound",
            description: "Product Line with given ID does not exist");
    }
}