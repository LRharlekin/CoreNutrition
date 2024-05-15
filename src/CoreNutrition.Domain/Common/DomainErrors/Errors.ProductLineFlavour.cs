using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class ProductLineFlavour
    {
        public static Error InvalidProductLineFlavourId => Error.Validation(
            code: "ProductLineFlavour.InvalidId",
            description: "Product Line Flavour ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "ProductLineFlavour.NotFound",
            description: "Product Line Flavour with given ID does not exist");
    }
}