namespace CoreNutrition.Contracts.Category;

public record UpsertCategoryRequest(
string Name,
string Description,
string CategoryImageUrl,
List<string> ProductLineIds
);