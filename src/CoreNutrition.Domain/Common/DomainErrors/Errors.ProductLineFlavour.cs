using ErrorOr;

using CoreNutrition.Domain.ProductLineFlavourAggregate;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class ProductLineFlavour
    {
        public static Error InvalidName => Error.Validation(
        code: "Flavour.InvalidName",
        description: $"The name for a Flavour must be between {ProductLineFlavourAggregate.ProductLineFlavour.MinNameLength} and {ProductLineFlavourAggregate.ProductLineFlavour.MaxNameLength} characters long.");

        public static Error InvalidProductLineFlavourId => Error.Validation(
            code: "ProductLineFlavour.InvalidId",
            description: "Product Line Flavour ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "ProductLineFlavour.NotFound",
            description: "Product Line Flavour with given ID does not exist");

        public static Error ListNotFound => Error.NotFound(
            code: "ProductLineFlavour.ListNotFound",
            description: "Not able to retrieve a list of product line flavours based on given parameters.");
    }
}