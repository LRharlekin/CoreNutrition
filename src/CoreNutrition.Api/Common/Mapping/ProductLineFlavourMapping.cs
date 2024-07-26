using Mapster;

using CoreNutrition.Contracts.ProductLineFlavour;
using CoreNutrition.Application.ProductLineFlavours.Commands.CreateProductLineFlavour;
// using CoreNutrition.Application.ProductLineFlavours.Commands.UpdateProductLineFlavour;
using CoreNutrition.Application.ProductLineFlavours.Queries.GetProductLineFlavourById;
using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Api.Common.Mapping;

public class ProductLineFlavourMapping : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    /* commands */

    config.NewConfig<CreateProductLineFlavourRequest, CreateProductLineFlavourCommand>()
      .Map((dest) => dest, (src) => src);

    // config.NewConfig<(Guid ProductLineFlavourId, UpdateProductLineFlavourRequest Request), UpdateProductLineFlavourCommand>()
    //   .Map((dest) => dest.Id, (src) => src.ProductLineFlavourId)
    //   .Map((dest) => dest, (src) => src.Request);

    /* queries */

    config.NewConfig<Guid, GetProductLineFlavourByIdQuery>()
      .Map((dest) => dest.Id, (src) => src);

    /* value objects */

    config.NewConfig<Guid, ProductLineFlavourId>()
      .MapWith(guid => ProductLineFlavourId.Create(guid));

    config.NewConfig<string, ProductLineFlavourId>()
      .MapWith(guidString => ProductLineFlavourId.Create(guidString).Value);

    config.NewConfig<Guid, ProductLineId>()
      .MapWith(guid => ProductLineId.Create(guid));

    config.NewConfig<string, ProductLineId>()
      .MapWith(guidString => ProductLineId.Create(guidString).Value);

    /* responses */

    // config.NewConfig<List<ProductLineFlavour>, ListProductLineFlavoursResponse>()
    //   .Map((dest) => dest, (src) => src);

    config.NewConfig<ProductLineFlavour, ProductLineFlavourResponse>()
      .Map((dest) => dest.Id, (src) => src.Id.Value.ToString())
      .Map((dest) => dest.ProductLineId, (src) => src.ProductLineId) // FK
      .Map((dest) => dest, (src) => src);
  }
}