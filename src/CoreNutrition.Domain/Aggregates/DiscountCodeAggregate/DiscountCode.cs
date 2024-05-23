using CoreNutrition.Domain.Common.Models;

namespace CoreNutrition.Domain.DiscountCodeAggregate;

public sealed class DiscountCode : AggregateRoot<DiscountCodeId, Guid>
{
  public string Code { get; }
  public float DiscountRate { get; }
  public DateTime StartDateTime { get; }
  public DateTime ExpiryDateTime { get; }
  public DateTime CreatedDateTime { get; }
  public DateTime UpdatedDateTime { get; }

}