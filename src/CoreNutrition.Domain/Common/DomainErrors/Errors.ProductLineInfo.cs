using ErrorOr;

using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
  public static class ProductLineInfo
  {
    public static Error InvalidShortDescription => Error.Validation(
    code: "ProductLineInfo.InvalidShortDescription",
    description: $"The Short Description text for a Product Line must be between {ProductLineAggregate.ValueObjects.ProductLineInfo.Constraints.MinDescriptionShortLength} and {ProductLineAggregate.ValueObjects.ProductLineInfo.Constraints.MaxDescriptionShortLength} characters long.");

    public static Error InvalidLongDescription => Error.Validation(
    code: "ProductLineInfo.InvalidLongDescription",
    description: $"The Long Description text for a Product Line must be between {ProductLineAggregate.ValueObjects.ProductLineInfo.Constraints.MinDescriptionLongLength} and {ProductLineAggregate.ValueObjects.ProductLineInfo.Constraints.MaxDescriptionLongLength} characters long.");

    public static Error InvalidSuggestedUse => Error.Validation(
    code: "ProductLineInfo.InvalidSuggestedUse",
    description: $"The Suggested Use text for a Product Line must be between {ProductLineAggregate.ValueObjects.ProductLineInfo.Constraints.MinSuggestedUseLength} and {ProductLineAggregate.ValueObjects.ProductLineInfo.Constraints.MaxSuggestedUseLength} characters long.");

    public static Error InvalidBenefitLength => Error.Validation(
    code: "ProductLineInfo.InvalidBenefitLength",
    description: $"The text for Benefits 1-3 for a Product Line must be no longer than {ProductLineAggregate.ValueObjects.ProductLineInfo.Constraints.MaxBenefitLength} characters long.");

    public static Error DuplicateBenefits => Error.Validation(
    code: "ProductLineInfo.DuplicateBenefits",
    description: "Benefits 1-3 must be different from each other.");

    public static Error DietGoalRequired => Error.Validation(
    code: "ProductLineInfo.DietGoalRequired",
    description: "At least one applicable diet goal must be specified for a Product Line: Muscle Gain, Weight Loss, or Health & Wellness.");
  }
}