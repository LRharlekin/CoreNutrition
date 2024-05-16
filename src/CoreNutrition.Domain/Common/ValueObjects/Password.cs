using System;
using System.Collections.Generic;
using System.Linq;

using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;


namespace EventReminder.Domain.Common.ValueObjects;

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

  public static ErrorOr<Password> CreateNew(string passwordString)
  {
    // perform validation on all conditions before returning an ErrorOr object that contains all errors than apply
    List<Error> errors = [];
    if (string.IsNullOrWhiteSpace(passwordString))
    {
      errors.Add(Errors.Password.NullOrEmpty);
    }
    if (passwordString.Length < MinPasswordLength)
    {
      errors.Add(Errors.Password.TooShort);
    }
    if (!passwordString.Any(IsLower))
    {
      errors.Add(Errors.Password.MissingLowercaseLetter);
    }
    if (!passwordString.Any(IsUpper))
    {
      errors.Add(Errors.Password.MissingUppercaseLetter);
    }
    if (!passwordString.Any(IsDigit))
    {
      errors.Add(Errors.Password.MissingDigit);
    }
    if (!passwordString.Any(IsNonAlphaNumeric))
    {
      errors.Add(Errors.Password.MissingNonAlphaNumeric);
    }
    if (errors.Count > 0)
    {
      return errors;
    }

    return new Password(passwordString);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
