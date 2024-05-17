// using CoreNutrition.Application.Category.Commands.XActionCommand;
// using CoreNutrition.Application.X.Queries.XActionQuery;
// using CoreNutrition.Application.X.Common;
// using CoreNutrition.Domain.XAggregate;

using Mapster;

/* disambiguation */
// using XContainedEntity = CoreNutrition.Domain.XAggregate.Entities;

namespace CoreNutrition.Api.Common.Mapping;

public class XMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    // config.NewConfig < (XSource), XDestination > ()
    //   .Map(dest => dest, src => src);
  }
}