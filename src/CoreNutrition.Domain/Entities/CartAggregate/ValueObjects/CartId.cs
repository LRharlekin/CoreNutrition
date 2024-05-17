using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

using ErrorOr;

namespace CoreNutrition.Domain.CartAggregate.ValueObjects;

public sealed class CartId : AggregateRootId<Guid>
{
  private CartId(Guid value) : base(value)
  {
  }

  public static CartId CreateUnique()
  {
    // TODO: enforce invariants
    return new CartId(Guid.NewGuid());
  }

  public static CartId Create(Guid value)
  {
    // TODO: enforce invariants
    return new CartId(value);
  }

  public static ErrorOr<CartId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.Cart.InvalidCartId;
    }

    return new CartId(guid);
  }
}