namespace CoreNutrition.Contracts.Category;

public record ListCategoriesRequest(
  int Page,
  int PageSize,
  string? SortColumn,
  string? SortOrder,
  string? SearchTerm
);