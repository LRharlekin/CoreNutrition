using System;
using System.Collections.Generic;
using System.Linq;

using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;


namespace CoreNutrition.Domain.UserAggregate.ValueObjects;

public sealed class Password : ValueObject
{
  private const int MinPasswordLength = 8;
  private static readonly Func<char, bool> IsLower = c => c >= 'a' && c <= 'z';
  private static readonly Func<char, bool> IsUpper = c => c >= 'A' && c <= 'Z';
  private static readonly Func<char, bool> IsDigit = c => c >= '0' && c <= '9';
  private static readonly Func<char, bool> IsNonAlphaNumeric = c => !(IsLower(c) || IsUpper(c) || IsDigit(c));

  private Password(string value)
  {
    Value = value;
  }

  public string Value { get; }

  public static implicit operator string(Password password) => password?.Value ?? string.Empty;

  public static ErrorOr<Password> CreateNew(string password)
  {
    // perform validation on all conditions before returning an ErrorOr object that contains all errors than apply
    List<Error> errors = [];
    if (string.IsNullOrWhiteSpace(password))
    {
      errors.Add(Errors.Password.NullOrEmpty);
    }
    if (password.Length < MinPasswordLength)
    {
      errors.Add(Errors.Password.TooShort);
    }
    if (!password.Any(IsLower))
    {
      errors.Add(Errors.Password.MissingLowercaseLetter);
    }
    if (!password.Any(IsUpper))
    {
      errors.Add(Errors.Password.MissingUppercaseLetter);
    }
    if (!password.Any(IsDigit))
    {
      errors.Add(Errors.Password.MissingDigit);
    }
    if (!password.Any(IsNonAlphaNumeric))
    {
      errors.Add(Errors.Password.MissingNonAlphaNumeric);
    }
    if (errors.Count > 0)
    {
      return errors;
    }

    return new Password(password);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
