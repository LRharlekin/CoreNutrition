using ErrorOr;

using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.Entities;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class SizeVariant
    {
        public static Error InvalidUnits => Error.Validation(
            code: "SizeVariant.InvalidUnits",
            description: $"Size Variant must contain at least {ProductLineSizeAggregate.Entities.SizeVariant.MinUnits} unit(s).");

        public static Error MissingSingleSizeReference => Error.Validation(
            code: "SizeVariant.MissingSingleSizeVariantId",
            description: "Single Size Variant ID is required for Multipacks. Please reference a size variant that contains the single unit specification of this product line.");

        public static Error InvalidWeightOrVolume => Error.Validation(
            code: "SizeVariant.InvalidWeightOrVolume",
            description: "Weight/Volume must be greater than 0.");

        public static Error WeightAndVolumeNotAllowed => Error.Validation(
            code: "SizeVariant.WeightAndVolumeNotAllowed",
            description: "Size can be specified either in Grams or Milliliters, but not both.");

        public static Error MissingWeightOrVolume => Error.Validation(
            code: "SizeVariant.MissingWeightOrVolume",
            description: "Either \"Unit Weight in Grams\" or \"Unit Volume in Milliliters\" are required to specify the size variant.");

        public static Error InvalidName => Error.Validation(
        code: "SizeVariant.InvalidName",
        description: $"Size Variant name must be between {ProductLineSizeAggregate.Entities.SizeVariant.MinNameLength} and {ProductLineSizeAggregate.Entities.SizeVariant.MaxNameLength} characters long.");

        public static Error InvalidDescription => Error.Validation(
        code: "SizeVariant.InvalidUnitAmount",
        description: $"Size Variant must contain at least {ProductLineSizeAggregate.Entities.SizeVariant.MinUnits} unit.");

        public static Error InvalidSizeVariantId => Error.Validation(
            code: "SizeVariant.InvalidId",
            description: "Size Variant ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "SizeVariant.NotFound",
            description: "Size Variant with given ID does not exist");
    }
}