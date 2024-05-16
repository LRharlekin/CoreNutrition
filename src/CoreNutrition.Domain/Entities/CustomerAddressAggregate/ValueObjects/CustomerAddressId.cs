using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

using ErrorOr;

namespace CoreNutrition.Domain.CustomerAddressAggregate.ValueObjects;

public sealed class CustomerAddressId : AggregateRootId<Guid>
{
  private CustomerAddressId(Guid value) : base(value)
  {
  }

  public static CustomerAddressId CreateUnique()
  {
    // TODO: enforce invariants
    return new CustomerAddressId(Guid.NewGuid());
  }

  public static CustomerAddressId Create(Guid value)
  {
    // TODO: enforce invariants
    return new CustomerAddressId(value);
  }

  public static ErrorOr<CustomerAddressId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.CustomerAddress.InvalidCustomerAddressId;
    }

    return new CustomerAddressId(guid);
  }
}