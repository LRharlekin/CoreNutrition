using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.ProductLineFlavourAggregate;

namespace CoreNutrition.Application.ProductLineFlavours.Queries.ListProductLineFlavours;

[Authorize(Roles = "Admin")]
public record ListProductLineFlavoursQuery(
  )
  : IAuthorizeableAction<ErrorOr<List<ProductLineFlavour>>>;