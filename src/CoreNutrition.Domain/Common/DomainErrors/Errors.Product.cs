using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Product
    {
        public static Error InvalidProductId => Error.Validation(
            code: "Product.InvalidId",
            description: "Product ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "Product.NotFound",
            description: "Product with given ID does not exist");
    }
}