
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Application.Category.Common;

namespace CoreNutrition.Application.Category.Commands.ActionCategory;

[Authorize(Roles = "Admin")]
public record CreateCategoryCommand(
  Guid UserId
  )
  : IAuthorizeableAction<ErrorOr<CategoryResult>>;