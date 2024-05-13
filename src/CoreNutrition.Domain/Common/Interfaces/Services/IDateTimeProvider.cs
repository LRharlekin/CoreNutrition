namespace CoreNutrition.Domain.Common.Interfaces.Services;

// "single source of truth" for entire application to obtain the current time
public interface IDateTimeProvider
{
  public DateTime UtcNow { get; }
}