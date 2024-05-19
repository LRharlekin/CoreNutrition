using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.CategoryAggregate;

namespace CoreNutrition.Application.Categories.Queries.ListCategories;

[Authorize(Roles = "Admin")]
public record ListCategoriesQuery(
  )
  : IAuthorizeableAction<ErrorOr<List<Category>>>;