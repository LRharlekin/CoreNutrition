using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

using ErrorOr;

namespace CoreNutrition.Domain.UserAggregate.ValueObjects;

public sealed class UserId : AggregateRootId<Guid>
{
  private UserId(Guid value) : base(value)
  {
  }

  public static UserId CreateUnique()
  {
    // TODO: enforce invariants
    return new UserId(Guid.NewGuid());
  }

  public static UserId Create(Guid value)
  {
    // TODO: enforce invariants
    return new UserId(value);
  }

  public static ErrorOr<UserId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.User.InvalidUserId;
    }

    return new UserId(guid);
  }
}