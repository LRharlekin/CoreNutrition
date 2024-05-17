using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class ShippingMethod
    {
        public static Error InvalidShippingMethodId => Error.Validation(
        code: "ShippingMethod.InvalidId",
        description: "Shipping Method ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "ShippingMethod.NotFound",
            description: "Shipping Method with given ID does not exist");
    }
}