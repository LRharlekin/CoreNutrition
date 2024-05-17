using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class ProductLine
    {
        public static Error InvalidProductLineId => Error.Validation(
            code: "ProductLine.InvalidId",
            description: "Product Line ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "ProductLine.NotFound",
            description: "Product Line with given ID does not exist");
    }
}