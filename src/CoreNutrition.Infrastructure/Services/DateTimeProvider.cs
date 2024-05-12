using CoreNutrition.Application.Common.Interfaces.Services;

namespace CoreNutrition.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
  public DateTime UtcNow => DateTime.UtcNow;
}