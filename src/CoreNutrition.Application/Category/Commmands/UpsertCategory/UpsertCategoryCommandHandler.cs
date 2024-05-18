using MediatR;
using ErrorOr;

using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Application.Common.Interfaces.Cryptography;
using CoreNutrition.Application.Common.Interfaces.Authentication;

namespace CoreNutrition.Application.Categories.Commands.UpsertCategory;

internal sealed class UpsertCategoryCommandHandler
  : IRequestHandler<UpsertCategoryCommand, ErrorOr<Category>>
{
  private readonly ICategoryRepository _categoryRepository;

  public UpsertCategoryCommandHandler(
    ICategoryRepository categoryRepository
  )
  {
    _categoryRepository = categoryRepository;
  }

  public async Task<ErrorOr<Category>> Handle(
    UpsertCategoryCommand command,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later
    /* perform action */
    var category = _categoryRepository.GetById(command.Id);
    /* persist changes */
    // _categoryRepository.Method(category)

    /* return result */
    // return new UpsertCategoryResult(category);
    return default!;
  }
}