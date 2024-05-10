// TODO: using Domain Errors namespace
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.Common.Models;

using ErrorOr;

namespace CoreNutrition.Domain.DiscountCodeAggregate;

public sealed class DiscountCodeId : AggregateRootId<Guid>
{
  public override Guid Value { get; protected set; }

  private DiscountCodeId(Guid value)
  {
    Value = value;
  }

  public static DiscountCodeId CreateUnique()
  {
    return new(Guid.NewGuid());
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

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }

#pragma warning disable CS8618
  public DiscountCodeId()
  {
    // Required by EF
  }
#pragma warning disable CS8618
}