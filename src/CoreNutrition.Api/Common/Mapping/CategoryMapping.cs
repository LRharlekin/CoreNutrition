using Mapster;

using CoreNutrition.Contracts.Category;
using CoreNutrition.Application.Categories.Commands.CreateCategory;
using CoreNutrition.Domain.CategoryAggregate;

namespace CoreNutrition.Api.Common.Mapping;

public class CategoryMapping : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<CreateCategoryRequest, CreateCategoryCommand>()
      .Map(dest => dest, src => src);

    config.NewConfig<Category, CategoryResponse>()
      .Map(dest => dest.Id, (src) => src.Id.Value.ToString())
      .Map(dest => dest.ProductLineIds, (src) => src.ProductLineIds.Select(productLineId => productLineId.Value))
      .Map(dest => dest, src => src);
  }
}