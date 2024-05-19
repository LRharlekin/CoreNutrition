
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Application.Categories.Commands.UpdateCategory;

[Authorize(Roles = "Admin")]
public record UpdateCategoryCommand(
  CategoryId Id,
  string Name,
  string Description,
  string CategoryImageUrl
  )
  : IAuthorizeableAction<ErrorOr<Category>>;