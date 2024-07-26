using ErrorOr;

using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.Common.Models;

namespace CoreNutrition.Domain.CartAggregate.ValueObjects;

public sealed class CartItemId : EntityId<Guid>
{
  private CartItemId(Guid value) : base(value)
  {
  }
  public static CartItemId CreateUnique()
  {
    return new CartItemId(Guid.NewGuid());
  }

  public static CartItemId Create(Guid value)
  {
    return new CartItemId(value);
  }

  public static ErrorOr<CartItemId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.CartItem.InvalidCartItemId;
    }

    return new CartItemId(guid);
  }
}