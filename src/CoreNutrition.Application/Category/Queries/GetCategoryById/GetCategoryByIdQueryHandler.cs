using MediatR;
using ErrorOr;

using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Application.Categories.Queries.GetCategoryById;

internal sealed class GetCategoryByIdQueryHandler
  : IRequestHandler<GetCategoryByIdQuery, ErrorOr<Category>>
{
  private readonly ICategoryRepository _categoryRepository;

  public GetCategoryByIdQueryHandler(
    ICategoryRepository categoryRepository
  )
  {
    _categoryRepository = categoryRepository;
  }

  public async Task<ErrorOr<Category>> Handle(
    GetCategoryByIdQuery query,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later
    var category = _categoryRepository.GetById(query.Id);

    if (category is null)
    {
      return Errors.Category.NotFound;
    }

    return category;
  }
}