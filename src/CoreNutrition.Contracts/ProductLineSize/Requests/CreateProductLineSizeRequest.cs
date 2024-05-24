namespace CoreNutrition.Contracts.ProductLineSize;

public record CreateProductLineSizeRequest(
  string ProductLineId,
  RecommendedRetailPriceRequest RecommendedRetailPrice,
  SizeVariantRequest SizeVariant
);

public record RecommendedRetailPriceRequest(
  decimal Amount,
  string CurrencyCode
);

public record SizeVariantRequest(
  string? SizeVariantId,
  string Name,
  int Units,
  int? UnitWeightInGrams,
  int? UnitVolumeInMilliliters,
  string? SingleSizeVariantId
);