using CoreNutrition.Domain.Common.Interfaces;

namespace CoreNutrition.Domain.ProductLineFlavourAggregate.Events;

public record ProductLineFlavourCreated(ProductLineFlavour ProductLineFlavour) : IDomainEvent;