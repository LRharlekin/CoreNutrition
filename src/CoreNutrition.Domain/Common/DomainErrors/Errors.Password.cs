using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
  public static class Password
  {
    public static Error NullOrEmpty => Error.Validation(
        code: "Password.NullOrEmpty",
        description: "Password is required");

    public static Error TooShort => Error.Validation(
        code: "Password.TooShort",
        description: "Password must be 8 characters or longer");

    public static Error MissingLowercaseLetter => Error.Validation(
        code: "Password.MissingLowercaseLetter",
        description: "Password must contain at least one lowercase letter");

    public static Error MissingUppercaseLetter => Error.Validation(
        code: "Password.MissingUppercaseLetter",
        description: "Password must contain at least one uppercase letter");

    public static Error MissingDigit => Error.Validation(
        code: "Password.MissingDigit",
        description: "Password must contain at least one digit (0-9)");

    public static Error MissingNonAlphaNumeric => Error.Validation(
        code: "Password.MissingNonAlphaNumeric",
        description: "Password must contain at least one non-alphanumeric character");
  }
}