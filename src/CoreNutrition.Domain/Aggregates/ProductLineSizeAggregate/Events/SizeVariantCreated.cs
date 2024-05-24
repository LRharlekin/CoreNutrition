using CoreNutrition.Domain.Common.Interfaces;
using CoreNutrition.Domain.ProductLineSizeAggregate.Entities;

namespace CoreNutrition.Domain.ProductLineSizeAggregate.Events;

public record SizeVariantCreated(SizeVariant SizeVariant) : IDomainEvent;