namespace CoreNutrition.Contracts.Product;

public record ProductResponse(
  string Id,
  string Name,
  bool IsPublished,
  AverageRatingResponse AverageRating,
  RetailPriceResponse RetailPrice,
  int QuantityInStock,
  string ProductLineId,
  string ProductLineSizeId,
  string ProductLineFlavourId,
  bool IsVegan,
  bool IsSample,
  string ProductImageUrl,
  List<string> CartItemIds,
  List<string> ReviewIds,
  List<string> OrderLineItemIds,
  DateTime CreatedDateTime,
  DateTime UpdatedDateTime
);

public record AverageRatingResponse(
  double? Score,
  int NumRatings
);

public record RetailPriceResponse
(
  decimal Amount,
  string CurrencyCode
);