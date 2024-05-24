using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Domain.UserAggregate.ValueObjects;

public sealed class LastName : ValueObject
{
  public const int MaxLength = 100;

  private LastName(string value) => Value = value;

  public string Value { get; }

  public static implicit operator string(LastName lastName) => lastName.Value;

  public static ErrorOr<LastName> CreateNew(string lastName)
  {
    if (string.IsNullOrWhiteSpace(lastName))
    {
      return Errors.LastName.NullOrEmpty;
    }
    if (lastName.Length > MaxLength)
    {
      return Errors.LastName.LongerThanAllowed;
    }

    return new LastName(lastName);
  }

  public override string ToString() => Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
