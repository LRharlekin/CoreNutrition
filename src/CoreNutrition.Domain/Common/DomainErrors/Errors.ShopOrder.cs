using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class ShopOrder
    {
        public static Error InvalidShopOrderId => Error.Validation(
            code: "ShopOrder.InvalidId",
            description: "Order ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "ShopOrder.NotFound",
            description: "Order with given ID does not exist");
    }
}