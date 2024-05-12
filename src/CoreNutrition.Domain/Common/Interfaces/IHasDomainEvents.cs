namespace CoreNutrition.Domain.Common.Interfaces;

public interface IHasDomainEvents
{
  public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

  public void ClearDomainEvents();
}