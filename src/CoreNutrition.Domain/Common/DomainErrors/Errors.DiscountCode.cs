using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
  public static class DiscountCode
  {
    public static Error InvalidDiscountCodeId => Error.Validation(
        code: "DiscountCode.InvalidId",
        description: "Discount Code ID is invalid");

    public static Error NotFound => Error.NotFound(
        code: "DiscountCode.NotFound",
        description: "Discount Code with given ID does not exist");
  }
}