namespace CoreNutrition.Contracts.ProductLineSize;

public record ProductLineSizeResponse(
  string Id,
  SizeVariantResponse SizeVariant,
  string ProductLineId,
  RecommendedRetailPriceResponse RecommendedRetailPrice,
  DateTime CreatedDateTime,
  DateTime UpdatedDateTime
);

public record SizeVariantResponse(
  string Id,
  string Name,
  int Units,
  int? UnitWeightInGrams,
  int? UnitVolumeInMilliliters,
  string SingleSizeId
);

public record RecommendedRetailPriceResponse(
  decimal Amount,
  string CurrencyCode);