using ErrorOr;

using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.Common.Models;

namespace CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;

public sealed class SizeVariantId : EntityId<Guid>
{
  private SizeVariantId(Guid value) : base(value)
  {
  }
  public static SizeVariantId CreateUnique()
  {
    return new SizeVariantId(Guid.NewGuid());
  }

  public static SizeVariantId Create(Guid value)
  {
    return new SizeVariantId(value);
  }

  public static ErrorOr<SizeVariantId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.SizeVariant.InvalidSizeVariantId;
    }

    return new SizeVariantId(guid);
  }
}