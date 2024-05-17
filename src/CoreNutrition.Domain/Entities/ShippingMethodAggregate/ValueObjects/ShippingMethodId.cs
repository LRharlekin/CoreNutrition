using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

using ErrorOr;

namespace CoreNutrition.Domain.ShippingMethodAggregate.ValueObjects;

public sealed class ShippingMethodId : AggregateRootId<Guid>
{
  private ShippingMethodId(Guid value) : base(value)
  {
  }

  public static ShippingMethodId CreateUnique()
  {
    // TODO: enforce invariants
    return new ShippingMethodId(Guid.NewGuid());
  }

  public static ShippingMethodId Create(Guid value)
  {
    // TODO: enforce invariants
    return new ShippingMethodId(value);
  }

  public static ErrorOr<ShippingMethodId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.ShippingMethod.InvalidShippingMethodId;
    }

    return new ShippingMethodId(guid);
  }
}