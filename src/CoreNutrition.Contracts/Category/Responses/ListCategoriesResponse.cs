namespace CoreNutrition.Contracts.Category;

public record ListCategoriesResponse(
  List<CategoryResponse> Categories,
  int TotalRecords = 10,
  int PageSize = 10
);