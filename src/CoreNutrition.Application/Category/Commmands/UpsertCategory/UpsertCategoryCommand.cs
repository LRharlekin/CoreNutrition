
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Application.Categories.Commands.UpsertCategory;

[Authorize(Roles = "Admin")]
public record UpsertCategoryCommand(
  CategoryId Id,
  string Name,
  string Description,
  string CategoryImageUrl,
  List<ProductLineId> ProductLineIds
  )
  : IAuthorizeableAction<ErrorOr<Category>>;