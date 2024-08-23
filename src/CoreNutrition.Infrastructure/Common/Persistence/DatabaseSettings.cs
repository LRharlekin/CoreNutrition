namespace CoreNutrition.Infrastructure.Common.Persistence;

public class DatabaseSettings
{
  public const string SectionName = "ConnectionStrings";
  public string DefaultConnection { get; set; } = string.Empty;
  public string Database { get; set; } = string.Empty;
}