using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

using ErrorOr;

namespace CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;

public sealed class ProductLineFlavourId : AggregateRootId<Guid>
{
  private ProductLineFlavourId(Guid value) : base(value)
  {
  }

  public static ProductLineFlavourId CreateUnique()
  {
    // TODO: enforce invariants
    return new ProductLineFlavourId(Guid.NewGuid());
  }

  public static ProductLineFlavourId Create(Guid value)
  {
    // TODO: enforce invariants
    return new ProductLineFlavourId(value);
  }

  public static ErrorOr<ProductLineFlavourId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.ProductLineFlavour.InvalidProductLineFlavourId;
    }

    return new ProductLineFlavourId(guid);
  }
}