using ErrorOr;

using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.Common.Models;

namespace CoreNutrition.Domain.ShopOrderAggregate.ValueObjects;

public sealed class OrderLineItemId : EntityId<Guid>
{
  private OrderLineItemId(Guid value) : base(value)
  {
  }
  public static OrderLineItemId CreateUnique()
  {
    return new OrderLineItemId(Guid.NewGuid());
  }

  public static OrderLineItemId Create(Guid value)
  {
    return new OrderLineItemId(value);
  }

  public static ErrorOr<OrderLineItemId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.OrderLineItem.InvalidOrderLineItemId;
    }

    return new OrderLineItemId(guid);
  }
}