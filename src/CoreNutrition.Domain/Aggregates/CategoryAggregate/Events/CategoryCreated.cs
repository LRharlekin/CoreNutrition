using CoreNutrition.Domain.Common.Interfaces;

namespace CoreNutrition.Domain.CategoryAggregate.Events;

public record CategoryCreated(Category Category) : IDomainEvent;