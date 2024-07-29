using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Domain.UserAggregate.ValueObjects;

public sealed class FirstName : ValueObject
{
  public static class Constraints
  {
    public const int MaxLength = 100;
  }

  private FirstName(string value) => Value = value;

  public string Value { get; }

  public static implicit operator string(FirstName firstName) => firstName.Value;

  public static ErrorOr<FirstName> CreateNew(string firstName)
  {
    var firstName = new FirstName(firstName);

    var errors = firstName.EnforceInvariants();

    if (errors.Count > 0)
    {
      return errors;
    }

    return firstName;
  }
  public override string ToString() => Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }

  private List<Error> EnforceInvariants()
  {
    var errors = new List<Error>();

    if (string.IsNullOrWhiteSpace(this.Value))
    {
      errors.Add(Errors.FirstName.NullOrEmpty);
    }

    if (this.Value.Length > Constraints.MaxLength)
    {
      errors.Add(Errors.FirstName.LongerThanAllowed);
    }

    return errors;
  }
}
