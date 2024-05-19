namespace CoreNutrition.Contracts.ProductLineFlavour;

public record ListProductLineFlavoursResponse(
  List<ProductLineFlavourResponse> ProductLineFlavours,
  int TotalRecords,
  int PageSize,
  int CurrentPage
);