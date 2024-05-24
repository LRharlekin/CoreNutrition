using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

using ErrorOr;

namespace CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;

public sealed class ProductLineSizeId : AggregateRootId<Guid>
{
  private ProductLineSizeId(Guid value) : base(value)
  {
  }

  public static ProductLineSizeId CreateUnique()
  {
    // TODO: enforce invariants
    return new ProductLineSizeId(Guid.NewGuid());
  }

  public static ProductLineSizeId Create(Guid value)
  {
    // TODO: enforce invariants
    return new ProductLineSizeId(value);
  }

  public static ErrorOr<ProductLineSizeId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.ProductLineSize.InvalidProductLineSizeId;
    }

    return new ProductLineSizeId(guid);
  }
}