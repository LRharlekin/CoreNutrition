namespace CoreNutrition.Contracts.Category;

public record ListCategoriesResponse(
  List<CategoryResponse> Categories,
  int TotalRecords,
  int PageSize
);