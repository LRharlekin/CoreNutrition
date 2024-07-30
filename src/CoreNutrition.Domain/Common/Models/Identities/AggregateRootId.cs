namespace CoreNutrition.Domain.Common.Models;

public abstract class AggregateRootId<TId> : EntityId<TId>
{
  protected AggregateRootId(TId value) : base(value)
  {
  }

#pragma warning disable CS8618
  protected AggregateRootId()
  {
  }
#pragma warning restore CS8618
}
