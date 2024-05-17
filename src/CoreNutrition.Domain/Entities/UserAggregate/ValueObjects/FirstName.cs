using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Domain.UserAggregate.ValueObjects;

public sealed class FirstName : ValueObject
{
  public const int MaxLength = 100;

  private FirstName(string value) => Value = value;

  public string Value { get; }

  public static implicit operator string(FirstName firstName) => firstName.Value;

  public static ErrorOr<FirstName> CreateNew(string firstName)
  {
    if (string.IsNullOrWhiteSpace(firstName))
    {
      return Errors.FirstName.NullOrEmpty;
    }
    if (firstName.Length > MaxLength)
    {
      return Errors.FirstName.LongerThanAllowed;
    }

    return new FirstName(firstName);
  }
  public override string ToString() => Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
