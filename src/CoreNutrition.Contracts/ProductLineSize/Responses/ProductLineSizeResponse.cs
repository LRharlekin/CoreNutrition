namespace CoreNutrition.Contracts.ProductLineSize;

public record ProductLineSizeResponse(
  string Id,
  string ProductLineId,
  SizeVariantResponse SizeVariant,
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
  string? SingleSizeVariantId
);

public record RecommendedRetailPriceResponse(
  decimal Amount,
  string CurrencyCode);