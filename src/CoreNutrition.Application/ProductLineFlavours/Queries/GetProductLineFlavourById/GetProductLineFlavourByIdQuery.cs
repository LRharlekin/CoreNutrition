
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.ProductLineFlavourAggregate;

namespace CoreNutrition.Application.ProductLineFlavours.Queries.GetProductLineFlavourById;

[Authorize(Roles = "Admin")]
public record GetProductLineFlavourByIdQuery(
  string Id
  )
  : IAuthorizeableAction<ErrorOr<ProductLineFlavour>>;