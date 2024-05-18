
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Application.Categories.Common;

namespace CoreNutrition.Application.Categories.Commands.CreateCategory;

[Authorize(Roles = "Admin")]
public record CreateCategoryCommand(
  string Name,
  string Description,
  string CategoryImageUrl
  )
  : IAuthorizeableAction<ErrorOr<CategoryResult>>;