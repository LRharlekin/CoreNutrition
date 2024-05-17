using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.Common.Models;

using ErrorOr;

namespace CoreNutrition.Domain.CustomerAggregate;

public sealed class CustomerId : AggregateRootId<Guid>
{
  private CustomerId(Guid value) : base(value)
  {
  }

  public static CustomerId CreateUnique()
  {
    return new CustomerId(Guid.NewGuid());
  }

  public static CustomerId Create(Guid value)
  {
    return new CustomerId(value);
  }

  public static ErrorOr<CustomerId> Create(string value)
  {
    return Guid.TryParse(value, out var guid)
      ? (ErrorOr<CustomerId>)Errors.Customer.InvalidCustomerId
      : (ErrorOr<CustomerId>)new CustomerId(guid);
  }
}