using CoreNutrition.Domain.Common.Models;

namespace CoreNutrition.Domain.UserAggregate;

public sealed class UserId : AggregateRootId<Guid>
{
  public override Guid Value { get; protected set; }

  private UserId(Guid value)
  {
    Value = value;
  }

  public static UserId CreateUnique()
  {
    return new UserId(Guid.NewGuid());
  }

  public static UserId Create(Guid userId)
  {
    return new UserId(userId);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }

  private UserId()
  {
    // Required by EF
  }
}