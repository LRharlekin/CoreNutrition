using CoreNutrition.Contracts.Category;
using CoreNutrition.Application.Categories.Commands.CreateCategory;

using CoreNutrition.Application.Categories.Common;
using CoreNutrition.Domain.CategoryAggregate;

using Mapster;

/* disambiguation */
// using XContainedEntity = CoreNutrition.Domain.XAggregate.Entities;

namespace CoreNutrition.Api.Common.Mapping;

public class CategoryMapping : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    /* request > command */
    config.NewConfig<CreateCategoryRequest, CreateCategoryCommand>()
      .Map(dest => dest, src => src);

    /* result ---> response */
    config.NewConfig<CategoryResult, CategoryResponse>()
      .Map(dest => dest.Id, (src) => src.Category.Id.Value.ToString())
      .Map(dest => dest.ProductLineIds, (src) => src.Category.ProductLineIds.Select(productLineId => productLineId.Value))
      .Map(dest => dest, src => src.Category);
  }
}