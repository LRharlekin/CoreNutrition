using MediatR;
using ErrorOr;

using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Application.Common.Interfaces.Cryptography;
using CoreNutrition.Application.Common.Interfaces.Authentication;

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
    Console.WriteLine("Handler before repo.GetById: " + query.Id);
    var category = _categoryRepository.GetById(query.Id);

    if (category is null)
    {
      return Errors.Category.NotFound;
    }
    Console.WriteLine("Handler after repo.GetById: " + category.Id);

    return category;
  }
}