using CoreNutrition.Domain.Common.Interfaces;

namespace CoreNutrition.Domain.ProductAggregate.Events;

public record ProductCreated(Product Product) : IDomainEvent;