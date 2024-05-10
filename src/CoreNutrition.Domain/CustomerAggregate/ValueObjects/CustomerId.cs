using CoreNutrition.Domain.Common.Models;

namespace CoreNutrition.Domain.CustomerAggregate;

public sealed class CustomerId : AggregateRootId<Guid>
{
  public override Guid Value { get; protected set; }

  private CustomerId(Guid value)
  {
    Value = value;
  }

  public static CustomerId CreateUnique()
  {
    return new CustomerId(Guid.NewGuid());
  }

  public static CustomerId Create(Guid customerId)
  {
    return new CustomerId(customerId);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }

  private CustomerId()
  {
    // Required by EF
  }
}