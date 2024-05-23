
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.CategoryAggregate;

namespace CoreNutrition.Application.Categories.Commands.UpdateCategory;

[Authorize(Roles = "Admin")]
public record UpdateCategoryCommand(
  string Id,
  string? Name,
  string? Description,
  string? CategoryImageUrl
  )
  : IAuthorizeableAction<ErrorOr<Category>>;