namespace CoreNutrition.Contracts.ProductLineFlavour;

public record ListProductLineFlavourssRequest(
  int Page,
  int PageSize,
  string? SortColumn,
  string? SortOrder,
  string? SearchTerm
);