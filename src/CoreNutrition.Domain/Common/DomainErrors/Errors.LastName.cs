using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
  public static class LastName
  {
    public static Error NullOrEmpty => Error.Validation(
        code: "LastName.NullOrEmpty",
        description: "Last name is required");

    public static Error LongerThanAllowed => Error.Validation(
        code: "LastName.LongerThanAllowed",
        description: "Last name is too long");
  }
}