namespace CoreNutrition.Contracts.ProductLineFlavour;

public record ListProductLineFlavoursRequest(
  int Page,
  int PageSize,
  string? SortColumn,
  string? SortOrder,
  string? SearchTerm
);