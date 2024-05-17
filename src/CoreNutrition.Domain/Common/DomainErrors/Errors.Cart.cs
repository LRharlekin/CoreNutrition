using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Cart
    {
        public static Error InvalidCartId => Error.Validation(
        code: "Cart.InvalidId",
        description: "Shopping Cart ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "Cart.NotFound",
            description: "Shopping Cart with given ID does not exist");
    }
}