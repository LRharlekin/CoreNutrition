using System.Text.RegularExpressions;

using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;


namespace CoreNutrition.Domain.UserAggregate.ValueObjects;

public sealed class Email : ValueObject
{
  public static class Constraints
  {
    public const int MaxLength = 255;
  }

  private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

  private static readonly Lazy<Regex> EmailFormatRegex =
      new Lazy<Regex>(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

  private Email(string value)
  {
    Value = value;
  }

  public string Value { get; }

  public static implicit operator string(Email email) => email.Value;

  public static ErrorOr<Email> CreateNew(string emailString)
  {
    var email = new Email(emailString);

    var errors = email.EnforceInvariants();

    if (errors.Count > 0)
    {
      return errors;
    }

    return email;
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
      errors.Add(Errors.Email.NullOrEmpty);
    }

    if (this.Value.Length > Constraints.MaxLength)
    {
      errors.Add(Errors.Email.LongerThanAllowed);
    }

    if (!EmailFormatRegex.Value.IsMatch(this.Value))
    {
      errors.Add(Errors.Email.InvalidFormat);
    }

    return errors;
  }
}
