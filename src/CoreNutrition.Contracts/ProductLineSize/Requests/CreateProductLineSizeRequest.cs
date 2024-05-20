namespace CoreNutrition.Contracts.ProductLineSize;

public record CreateProductLineSizeRequest(
  string ProductLineId,
  RecommendedRetailPrice RecommendedRetailPrice,
  SizeVariant SizeVariant
);

public record RecommendedRetailPrice(
  decimal Amount,
  string CurrencyCode
);

public record SizeVariant(
  string? SizeVariantId,
  string Name,
  int Units,
  int? UnitWeightInGrams,
  int? UnitVolumeInMilliliters,
  string? SingleSizeVariantId
);