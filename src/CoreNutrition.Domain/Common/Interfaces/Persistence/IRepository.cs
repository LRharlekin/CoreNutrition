using CoreNutrition.Domain.Common.Models;

namespace CoreNutrition.Domain.Common.Interfaces.Persistence;

public interface IRepository<T, TId, TIdType>
  where T : AggregateRoot<TId, TIdType>
  where TId : AggregateRootId<TIdType>
{ }