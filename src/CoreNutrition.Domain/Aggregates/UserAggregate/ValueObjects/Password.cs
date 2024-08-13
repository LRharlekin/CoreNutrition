using System;
using System.Collections.Generic;
using System.Linq;

using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;


namespace CoreNutrition.Domain.UserAggregate.ValueObjects;

public sealed class Password : ValueObject
{
  public static class Constraints
  {
    public const int MinPasswordLength = 8;
  }


  private Password(string value)
  {
    Value = value;
  }

  public string Value { get; }

  public static implicit operator string(Password password) => password?.Value ?? string.Empty;

  public static ErrorOr<Password> CreateNew(string passwordString)
  {
    var password = new Password(passwordString);

    var errors = password.EnforceInvariants();

    if (errors.Count > 0)
    {
      return errors;
    }

    return password;
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
      errors.Add(Errors.Password.NullOrEmpty);
    }
    if (this.Value.Length < Constraints.MinPasswordLength)
    {
      errors.Add(Errors.Password.TooShort);
    }
    if (!this.Value.Any(IsLower))
    {
      errors.Add(Errors.Password.MissingLowercaseLetter);
    }
    if (!this.Value.Any(IsUpper))
    {
      errors.Add(Errors.Password.MissingUppercaseLetter);
    }
    if (!this.Value.Any(IsDigit))
    {
      errors.Add(Errors.Password.MissingDigit);
    }
    if (!this.Value.Any(IsNonAlphaNumeric))
    {
      errors.Add(Errors.Password.MissingNonAlphaNumeric);
    }
    
    return errors;
  }

  private static readonly Func<char, bool> IsLower = c => c >= 'a' && c <= 'z';
  private static readonly Func<char, bool> IsUpper = c => c >= 'A' && c <= 'Z';
  private static readonly Func<char, bool> IsDigit = c => c >= '0' && c <= '9';
  private static readonly Func<char, bool> IsNonAlphaNumeric = c => !(IsLower(c) || IsUpper(c) || IsDigit(c));
}
