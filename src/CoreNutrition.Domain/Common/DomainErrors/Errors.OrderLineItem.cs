using ErrorOr;

using CoreNutrition.Domain.ShopOrderAggregate.ValueObjects;
using CoreNutrition.Domain.ShopOrderAggregate.Entities;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
  public static class OrderLineItem
  {
    public static Error InvalidOrderLineItemId => Error.Validation(
           code: "OrderLineItem.InvalidId",
           description: "Order Line Item ID is invalid");

    public static Error NotFound => Error.NotFound(
        code: "OrderLineItem.NotFound",
        description: "Order Line Item with given ID does not exist");
  }
}