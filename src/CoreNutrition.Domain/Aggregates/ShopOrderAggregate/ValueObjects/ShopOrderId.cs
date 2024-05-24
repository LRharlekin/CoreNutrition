using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

using ErrorOr;

namespace CoreNutrition.Domain.ShopOrderAggregate.ValueObjects;

public sealed class ShopOrderId : AggregateRootId<Guid>
{
  private ShopOrderId(Guid value) : base(value)
  {
  }

  // CreateUnique() when we need to generate a Guid
  public static ShopOrderId CreateUnique()
  {
    // TODO: enforce invariants
    return new ShopOrderId(Guid.NewGuid());
  }

  // Create() when we already have the Guid
  public static ShopOrderId Create(Guid value)
  {
    // TODO: enforce invariants
    return new ShopOrderId(value);
  }

  public static ErrorOr<ShopOrderId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.ShopOrder.InvalidShopOrderId;
    }

    return new ShopOrderId(guid);
  }
}