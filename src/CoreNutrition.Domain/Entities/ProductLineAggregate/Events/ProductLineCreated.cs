using CoreNutrition.Domain.Common.Interfaces;

namespace CoreNutrition.Domain.ProductLineAggregate.Events;

public record ProductLineCreated(ProductLine ProductLine) : IDomainEvent;