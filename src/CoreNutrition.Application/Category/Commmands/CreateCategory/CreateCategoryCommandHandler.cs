using MediatR;
using ErrorOr;

using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Application.Categories.Common;
using CoreNutrition.Application.Common.Interfaces.Cryptography;
using CoreNutrition.Application.Common.Interfaces.Authentication;

namespace CoreNutrition.Application.Categories.Commands.CreateCategory;

internal sealed class CreateCategoryCommandHandler
  : IRequestHandler<CreateCategoryCommand, ErrorOr<CategoryResult>>
{
  private readonly ICategoryRepository _categoryRepository;

  public CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository
  )
  {
    _categoryRepository = categoryRepository;
  }

  public async Task<ErrorOr<CategoryResult>> Handle(
    CreateCategoryCommand command,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later
    /* perform action */
    var category = Category.Create(
      command.Name,
      command.Description,
      command.CategoryImageUrl
    );

    /* persist changes */
    _categoryRepository.Add(category);

    /* return result */
    return new CategoryResult(category);
  }
}