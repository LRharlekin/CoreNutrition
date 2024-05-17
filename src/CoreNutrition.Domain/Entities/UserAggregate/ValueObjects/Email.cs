using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;


namespace CoreNutrition.Domain.UserAggregate.ValueObjects;

public sealed class Email : ValueObject
{
  public const int MaxLength = 256;

  private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

  private static readonly Lazy<Regex> EmailFormatRegex =
      new Lazy<Regex>(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

  private Email(string value)
  {
    Value = value;
  }

  public string Value { get; }

  public static implicit operator string(Email email) => email.Value;

  public static ErrorOr<Email> CreateNew(string email)
  {
    if (string.IsNullOrWhiteSpace(email))
    {
      return Errors.Email.NullOrEmpty;
    }
    if (email.Length > MaxLength)
    {
      return Errors.Email.LongerThanAllowed;
    }
    if (!EmailFormatRegex.Value.IsMatch(email))
    {
      return Errors.Email.InvalidFormat;
    }

    return new Email(email);
  }
  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
