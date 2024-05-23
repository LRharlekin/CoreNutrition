using MediatR;
using ErrorOr;

using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Application.Common.Interfaces.Cryptography;
using CoreNutrition.Application.Common.Interfaces.Authentication;

namespace CoreNutrition.Application.Categories.Commands.UpdateCategory;

internal sealed class UpdateCategoryCommandHandler
  : IRequestHandler<UpdateCategoryCommand, ErrorOr<Category>>
{
  private readonly ICategoryRepository _categoryRepository;

  public UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository
  )
  {
    _categoryRepository = categoryRepository;
  }

  public async Task<ErrorOr<Category>> Handle(
    UpdateCategoryCommand command,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later

    Guid.TryParse(command.Id, out Guid guid);
    CategoryId categoryId = CategoryId.Create(guid);

    Category? categoryResult = _categoryRepository.GetById(categoryId!);

    if (categoryResult is null)
    {
      return Errors.Category.NotFound;
    }

    // categoryResult.ChangeName(command.Name);
    // categoryResult.ChangeDescription(command.Description);
    // categoryResult.ChangeCategoryImageUrl(command.CategoryImageUrl);

    /* persist changes */
    // _categoryRepository.Method(category)
    // await _unitOfWork.SaveChangesAsync(cancellationToken);

    /* return result */
    // return categoryResult;
    return default!;
  }
}