
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;

namespace CoreNutrition.Application.ProductLineFlavours.Queries.GetProductLineFlavourById;

[Authorize(Roles = "Admin")]
public record GetProductLineFlavourByIdQuery(
  ProductLineFlavourId Id
  )
  : IAuthorizeableAction<ErrorOr<ProductLineFlavour>>;