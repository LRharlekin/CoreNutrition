using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.CategoryAggregate;

namespace CoreNutrition.Application.Categories.Commands.CreateCategory;

[Authorize(Roles = "Admin")]
public record CreateCategoryCommand(
  string Name,
  string Description,
  string CategoryImageUrl
  )
  : IAuthorizeableAction<ErrorOr<Category>>;