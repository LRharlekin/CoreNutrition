using Mapster;

using CoreNutrition.Contracts.ProductLine;
using CoreNutrition.Application.ProductLines.Commands.CreateProductLine;
// using CoreNutrition.Application.ProductLines.Commands.UpdateProductLine;
// using CoreNutrition.Application.ProductLines.Queries.GetProductLineById;
using CoreNutrition.Domain.ProductLineAggregate;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Api.Common.Mapping;

public class ProductLineMapping : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    /* commands */

    config.NewConfig<CreateProductLineRequest, CreateProductLineCommand>()
      .Map((dest) => dest, (src) => src);

    // config.NewConfig<(Guid ProductLineId, UpdateProductLineRequest Request), UpdateProductLineCommand>()
    //   .Map((dest) => dest.Id, (src) => src.ProductLineId)
    //   .Map((dest) => dest, (src) => src.Request);

    /* queries */

    // config.NewConfig<Guid, GetCategoryByIdQuery>()
    //   .Map((dest) => dest.Id, (src) => src);

    /* value objects */

    config.NewConfig<Guid, ProductLineId>()
      .MapWith(guid => ProductLineId.Create(guid));
    config.NewConfig<string, ProductLineId>()
      .MapWith(guidString => ProductLineId.Create(guidString).Value);

    /* responses */

    // config.NewConfig<List<ProductLine>, ListProductLinesResponse>()
    //   .Map((dest) => dest, (src) => src);

    config.NewConfig<ProductLine, ProductLineResponse>()
      .Map((dest) => dest.Id, (src) => src.Id.Value.ToString())
      .Map((dest) => dest.ProductIds, (src) => src.ProductIds.Select((productId) => productId.Value))
      .Map((dest) => dest.ProductLineSizeIds, (src) => src.ProductLineSizeIds.Select((productLineSizeId) => productLineSizeId.Value))
      .Map((dest) => dest.ProductLineFlavourIds, (src) => src.ProductLineFlavourIds.Select((productLineFlavourId) => productLineFlavourId.Value))
      .Map((dest) => dest, (src) => src);
  }
}