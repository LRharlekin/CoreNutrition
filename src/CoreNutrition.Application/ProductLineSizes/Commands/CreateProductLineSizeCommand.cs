
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.ProductLineSizeAggregate;
// using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
// using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Application.ProductLineSizes.Commands.CreateProductLineSize;

[Authorize(Roles = "Admin")]
public record CreateProductLineSizeCommand(
  string ProductLineId,
  RecommendedRetailPriceCommand RecommendedRetailPrice,
  SizeVariantCommand SizeVariant
) : IAuthorizeableAction<ErrorOr<ProductLineSize>>;

public record RecommendedRetailPriceCommand(
  decimal Amount,
  string CurrencyCode
);

public record SizeVariantCommand(
  string? SizeVariantId,
  string Name,
  int Units,
  int? UnitWeightInGrams,
  int? UnitVolumeInMilliliters,
  string? SingleSizeVariantId
);