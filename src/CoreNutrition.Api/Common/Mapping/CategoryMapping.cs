using Mapster;

using CoreNutrition.Contracts.Category;
using CoreNutrition.Application.Categories.Commands.CreateCategory;
using CoreNutrition.Application.Categories.Commands.UpdateCategory;
using CoreNutrition.Application.Categories.Queries.GetCategoryById;
using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Api.Common.Mapping;

public class CategoryMapping : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    /* commands */

    config.NewConfig<CreateCategoryRequest, CreateCategoryCommand>()
      .Map((dest) => dest, (src) => src);

    config.NewConfig<(Guid CategoryId, UpdateCategoryRequest Request), UpdateCategoryCommand>()
      .Map((dest) => dest.Id, (src) => src.CategoryId)
      .Map((dest) => dest, (src) => src.Request);

    /* queries */

    config.NewConfig<Guid, GetCategoryByIdQuery>()
      .Map((dest) => dest.Id, (src) => src);

    /* value objects */

    config.NewConfig<Guid, CategoryId>()
      .MapWith(guid => CategoryId.Create(guid));

    config.NewConfig<Guid, ProductLineId>()
      .MapWith(guid => ProductLineId.Create(guid));

    /* responses */

    config.NewConfig<List<Category>, ListCategoriesResponse>()
      .Map((dest) => dest, (src) => src);

    config.NewConfig<Category, CategoryResponse>()
      .Map((dest) => dest.Id, (src) => src.Id.Value.ToString())
      .Map((dest) => dest.ProductLineIds, (src) => src.ProductLineIds.Select((productLineId) => productLineId.Value))
      .Map((dest) => dest, (src) => src);
  }
}