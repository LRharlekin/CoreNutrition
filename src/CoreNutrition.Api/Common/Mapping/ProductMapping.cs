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

    config.NewConfig<Guid, ProductId>()
      .MapWith(guid => ProductId.Create(guid));

    /* responses */

    // config.NewConfig<List<Product>, ListProductsResponse>()
    //   .Map((dest) => dest, (src) => src);

  }
}