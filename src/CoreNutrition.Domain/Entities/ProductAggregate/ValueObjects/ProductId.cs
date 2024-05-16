using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

using ErrorOr;

namespace CoreNutrition.Domain.ProductAggregate.ValueObjects;

public sealed class ProductId : AggregateRootId<Guid>
{
  private ProductId(Guid value) : base(value)
  {
  }

  public static ProductId CreateUnique()
  {
    // TODO: enforce invariants
    return new ProductId(Guid.NewGuid());
  }

  public static ProductId Create(Guid value)
  {
    // TODO: enforce invariants
    return new ProductId(value);
  }

  public static ErrorOr<ProductId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.Product.InvalidProductId;
    }

    return new ProductId(guid);
  }
}