
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Application.ProductLineSizes.Commands.CreateProductLineSize;

[Authorize(Roles = "Admin")]
public record CreateProductLineSizeCommand(
  string Size,
  ProductLineId ProductLineId,
  string SizeImageUrl
  )
  : IAuthorizeableAction<ErrorOr<ProductLineSize>>;