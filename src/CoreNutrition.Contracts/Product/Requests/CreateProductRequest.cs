namespace CoreNutrition.Contracts.Product;

public record CreateProductRequest(
  string Name,
  bool IsPublished,
  // initial average rating = null,
  RetailPriceRequest RetailPrice,
  int QuantityInStock,
  string ProductLineSizeId,
  string ProductLineId,
  string ProductLineFlavourId,
  bool IsVegan,
  bool IsSample,
  string ProductImageUrl
);

public record RetailPriceRequest(
  decimal Amount,
  string CurrencyCode
);