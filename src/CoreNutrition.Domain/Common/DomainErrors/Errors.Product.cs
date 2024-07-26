using ErrorOr;

using CoreNutrition.Domain.ProductAggregate;

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

        public static Error InvalidNameLength => Error.Validation(
            code: "Product.InvalidNameLength",
            description: $"The Name for a Product must be between {ProductAggregate.Product.Constraints.MinNameLength} and {ProductAggregate.Product.Constraints.MaxNameLength} characters long.");

        public static Error InvalidRetailPrice => Error.Validation(
            code: "Product.InvalidRetailPrice",
            description: $"The Retail Price for a Product must be greater than {ProductAggregate.Product.Constraints.MinRetailPrice}.");

        public static Error InvalidQuantityInStock => Error.Validation(
            code: "Product.InvalidQuantityInStock",
            description: $"The Quantity in Stock for a Product must be greater than {ProductAggregate.Product.Constraints.MinQuantityInStock}.");
    }
}