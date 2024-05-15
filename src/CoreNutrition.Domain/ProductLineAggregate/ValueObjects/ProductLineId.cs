using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

using ErrorOr;

namespace CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

public sealed class ProductLineId : AggregateRootId<Guid>
{
  private ProductLineId(Guid value) : base(value)
  {
  }

  public static ProductLineId CreateUnique()
  {
    // TODO: enforce invariants
    return new ProductLineId(Guid.NewGuid());
  }

  public static ProductLineId Create(Guid value)
  {
    // TODO: enforce invariants
    return new ProductLineId(value);
  }

  public static ErrorOr<ProductLineId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.ProductLine.InvalidProductLineId;
    }

    return new ProductLineId(guid);
  }
}