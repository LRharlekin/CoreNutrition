using ErrorOr;

using CoreNutrition.Domain.CartAggregate.ValueObjects;
using CoreNutrition.Domain.CartAggregate.Entities;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
  public static class CartItem
  {
    public static Error InvalidCartItemId => Error.Validation(
           code: "CartItem.InvalidId",
           description: "Cart Item ID is invalid");

    public static Error NotFound => Error.NotFound(
        code: "CartItem.NotFound",
        description: "Cart Item with given ID does not exist");
  }
}