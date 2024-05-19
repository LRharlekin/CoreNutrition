

using MediatR;
using ErrorOr;

using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Application.Categories.Queries.ListCategories;

internal sealed class ListCategoriesQueryHandler
  : IRequestHandler<ListCategoriesQuery, ErrorOr<List<Category>>>
{
  private readonly ICategoryRepository _categoryRepository;

  public ListCategoriesQueryHandler(
    ICategoryRepository categoryRepository
  )
  {
    _categoryRepository = categoryRepository;
  }

  public async Task<ErrorOr<List<Category>>> Handle(
    ListCategoriesQuery query,
    CancellationToken cancellationToken)
  {

    /* pagination */
    // .Skip((query.Page - 1) * query.PageSize)
    // .Take(query.PageSize);

    await Task.CompletedTask; // TODO delete later
    var categoriesResult = _categoryRepository.GetAll();

    if (categoriesResult is null)
    {
      return Errors.Category.ListNotFound;
    }

    return categoriesResult;
  }
}