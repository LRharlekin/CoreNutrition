using CoreNutrition.Domain.Common.Interfaces;

namespace CoreNutrition.Domain.UserAggregate.Events;

public record UserCreated(User User) : IDomainEvent;