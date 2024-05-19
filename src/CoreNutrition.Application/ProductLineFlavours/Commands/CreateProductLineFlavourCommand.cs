
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Application.ProductLineFlavours.Commands.CreateProductLineFlavour;

[Authorize(Roles = "Admin")]
public record CreateProductLineFlavourCommand(
  string Flavour,
  ProductLineId ProductLineId,
  string FlavourImageUrl
  )
  : IAuthorizeableAction<ErrorOr<ProductLineFlavour>>;