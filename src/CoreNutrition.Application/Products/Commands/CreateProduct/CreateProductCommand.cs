using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.ProductAggregate;

namespace CoreNutrition.Application.Products.Commands.CreateProduct;

[Authorize(Roles = "Admin")]
public record CreateProductCommand(
  string Name,
  bool IsPublished,
  // initial average rating = null,
  RetailPriceCommand RetailPrice,
  int QuantityInStock,
  string ProductLineId,
  string ProductLineSizeId,
  string ProductLineFlavourId,
  bool IsVegan,
  bool IsSample,
  string ProductImageUrl
  )
  : IAuthorizeableAction<ErrorOr<Product>>;

public record RetailPriceCommand(
  decimal Amount,
  string CurrencyCode
);