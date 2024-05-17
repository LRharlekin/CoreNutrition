using CoreNutrition.Domain.Common.Models;

namespace CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;

public sealed class SizeId : EntityId<Guid>
{
  private SizeId(Guid value) : base(value)
  {
  }

  public static SizeId Create(Guid value)
  {
    return new SizeId(value);
  }
  public static SizeId CreateUnique()
  {
    return new SizeId(Guid.NewGuid());
  }
}