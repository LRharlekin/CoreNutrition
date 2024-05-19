namespace CoreNutrition.Contracts.ProductLineSize;

public record CreateProductLineSizeRequest(
  SizeVariant SizeVariant,
  string ProductLineId,
  CurrencyAmount RecommendedRetailPrice
);

public record SizeVariant(
  string? Id,
  string Name,
  int Units,
  int? UnitWeightInGrams,
  int? UnitVolumeInMilliliters,
  string? SingleSizeId
);

public record CurrencyAmount(
  decimal Amount,
  string CurrencyCode
);