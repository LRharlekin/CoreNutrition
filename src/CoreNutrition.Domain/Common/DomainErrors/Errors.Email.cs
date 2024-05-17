using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
  public static class Email
  {
    public static Error NullOrEmpty => Error.Validation(
        code: "Email.NullOrEmpty",
        description: "Email is required");

    public static Error LongerThanAllowed => Error.Validation(
        code: "Email.LongerThanAllowed",
        description: "Email is too long");

    public static Error InvalidFormat => Error.Validation(
        code: "Email.InvalidFormat",
        description: "Email has invalid format");
  }
}