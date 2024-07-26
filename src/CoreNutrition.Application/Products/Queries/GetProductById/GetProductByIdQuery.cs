
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.ProductAggregate;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;

namespace CoreNutrition.Application.Products.Queries.GetProductById;

[Authorize(Roles = "Admin")]
public record GetProductByIdQuery(
  Guid Id
  )
  : IAuthorizeableAction<ErrorOr<Product>>;