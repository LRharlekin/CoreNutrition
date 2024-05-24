using Mapster;

using CoreNutrition.Contracts.ProductLineSize;
using CoreNutrition.Application.ProductLineSizes.Commands.CreateProductLineSize;
// using CoreNutrition.Application.ProductLineSizes.Commands.UpdateProductLineSize;
// using CoreNutrition.Application.ProductLineSizes.Queries.GetProductLineSizeById;
using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.ProductLineSizeAggregate.Entities;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Api.Common.Mapping;

public class ProductLineSizeMapping : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    /* commands */

    config.NewConfig<CreateProductLineSizeRequest, CreateProductLineSizeCommand>()
      .Map((dest) => dest, (src) => src);

    // config.NewConfig<(Guid ProductLineSizeId, UpdateProductLineSizeRequest Request), UpdateProductLineSizeCommand>()
    //   .Map((dest) => dest.Id, (src) => src.ProductLineSizeId)
    //   .Map((dest) => dest, (src) => src.Request);

    /* queries */

    // config.NewConfig<Guid, GetProductLineSizeByIdQuery>()
    //   .Map((dest) => dest.Id, (src) => src);

    /* value objects */

    config.NewConfig<Guid, ProductLineSizeId>()
      .MapWith(guid => ProductLineSizeId.Create(guid));

    config.NewConfig<Guid, ProductLineSizeId>()
      .MapWith(guid => ProductLineSizeId.Create(guid));

    config.NewConfig<Guid, ProductLineId>()
      .MapWith(guid => ProductLineId.Create(guid));

    config.NewConfig<string, ProductLineId>()
      .MapWith(guidString => ProductLineId.Create(guidString).Value);

    /* responses */

    // config.NewConfig<List<ProductLineSize>, ListProductLineSizesResponse>()
    //   .Map((dest) => dest, (src) => src);

    config.NewConfig<ProductLineSize, ProductLineSizeResponse>()
      .Map((dest) => dest.Id, (src) => src.Id.Value.ToString())
      .Map((dest) => dest.ProductLineId, (src) => src.ProductLineId)
      .Map((dest) => dest, (src) => src);
  }
}