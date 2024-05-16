namespace CoreNutrition.Contracts.Category;

public record CreateCategoryRequest(
  string Name,
  string Description,
  string CategoryImageUrl
);