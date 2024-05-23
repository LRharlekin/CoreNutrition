
using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;

namespace CoreNutrition.Application.Categories.Queries.GetCategoryById;

[Authorize(Roles = "Admin")]
public record GetCategoryByIdQuery(
  string Id
  )
  : IAuthorizeableAction<ErrorOr<Category>>;