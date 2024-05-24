
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.ProductLineFlavourAggregate;

namespace CoreNutrition.Application.ProductLineFlavours.Commands.UpdateProductLineFlavour;

[Authorize(Roles = "Admin")]
public record UpdateProductLineFlavourCommand(
  string Id,
  string? Flavour,
  string? ProductLineId,
  string? FlavourImageUrl
  )
  : IAuthorizeableAction<ErrorOr<ProductLineFlavour>>;