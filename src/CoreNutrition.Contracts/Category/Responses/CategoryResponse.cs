namespace CoreNutrition.Contracts.Category;

public record CategoryResponse(
  string Id,
  string Name,
  string Description,
  string CategoryImageUrl,
  List<string> ProductLineIds,
  DateTime CreatedDateTime,
  DateTime UpdatedDateTime
);