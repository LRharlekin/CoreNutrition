namespace CoreNutrition.Contracts.Category;

public record CategoryListResponse(
  List<CategoryResponse> Categories
);