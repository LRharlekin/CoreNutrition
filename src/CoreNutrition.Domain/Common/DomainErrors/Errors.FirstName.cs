using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
  public static class FirstName
  {
    public static Error NullOrEmpty => Error.Validation(
        code: "FirstName.NullOrEmpty",
        description: "First name is required");

    public static Error LongerThanAllowed => Error.Validation(
        code: "FirstName.LongerThanAllowed",
        description: "First name is too long");
  }
}