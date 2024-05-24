using CoreNutrition.Domain.Common.Interfaces;

namespace CoreNutrition.Domain.ProductLineSizeAggregate.Events;

public record ProductLineSizeCreated(ProductLineSize ProductLineSize) : IDomainEvent;