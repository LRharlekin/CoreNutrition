using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Domain.UserAggregate.ValueObjects;

public sealed class LastName : ValueObject
{
  public static class Constraints
  {
    public const int MaxLength = 100;
  }

  private LastName(string value) => Value = value;

  public string Value { get; }

  public static implicit operator string(LastName lastName) => lastName.Value;

  public static ErrorOr<LastName> CreateNew(string lastNameString)
  {
    var lastName = new LastName(lastNameString);

    var errors = lastName.EnforceInvariants();

    if (errors.Count > 0)
    {
      return errors;
    }

    return lastName;
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
      errors.Add(Errors.LastName.NullOrEmpty);
    }
    if (this.Value.Length > Constraints.MaxLength)
    {
      errors.Add(Errors.LastName.LongerThanAllowed);
    }

    return errors;
  }
}
