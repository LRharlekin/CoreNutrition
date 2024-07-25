using Mapster;

using CoreNutrition.Contracts.Product;
using CoreNutrition.Application.Products.Commands.CreateProduct;
// using CoreNutrition.Application.ProductLines.Commands.UpdateProductLine;
// using CoreNutrition.Application.ProductLines.Queries.GetProductLineById;
using CoreNutrition.Domain.ProductAggregate;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;

namespace CoreNutrition.Api.Common.Mapping;

public class ProductMapping : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    /* commands */

    config.NewConfig<CreateProductRequest, CreateProductCommand>()
      .Map((dest) => dest, (src) => src);

    /* queries */

    /* value objects */

    config.NewConfig<Guid, ProductId>()
      .MapWith(guid => ProductId.Create(guid));

    /* responses */

    // config.NewConfig<List<Product>, ListProductsResponse>()
    //   .Map((dest) => dest, (src) => src);

    config.NewConfig<Product, ProductResponse>()
      .Map((dest) => dest.Id, (src) => src.Id.Value.ToString())
      .Map((dest) => dest.ReviewIds, (src) => src.ReviewIds.Select((reviewId) => reviewId.Value))
      .Map((dest) => dest.CartItemIds, (src) => src.CartItemIds.Select((cartItemId) => cartItemId.Value))
      .Map((dest) => dest.OrderLineItemIds, (src) => src.OrderLineItemIds.Select((orderLineItemId) => orderLineItemId.Value))
      .Map((dest) => dest, (src) => src);
  }
}