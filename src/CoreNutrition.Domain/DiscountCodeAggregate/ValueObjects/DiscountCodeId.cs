using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.Common.Models;

using ErrorOr;

namespace CoreNutrition.Domain.DiscountCodeAggregate;

public sealed class DiscountCodeId : AggregateRootId<Guid>
{
  private DiscountCodeId(Guid value) : base(value)
  {
  }

  public static DiscountCodeId CreateUnique()
  {
    return new DiscountCodeId(Guid.NewGuid());
  }

  public static DiscountCodeId Create(Guid value)
  {
    return new DiscountCodeId(value);
  }

  public static ErrorOr<DiscountCodeId> Create(string value)
  {
    return Guid.TryParse(value, out var guid)
      ? (ErrorOr<DiscountCodeId>)Errors.DiscountCode.InvalidDiscountCodeId
      : (ErrorOr<DiscountCodeId>)new DiscountCodeId(guid);
  }
}