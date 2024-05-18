using MediatR;
using ErrorOr;

using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.Common.Interfaces.Persistence;

namespace CoreNutrition.Application.Categories.Commands.CreateCategory;

internal sealed class CreateCategoryCommandHandler
  : IRequestHandler<CreateCategoryCommand, ErrorOr<Category>>
{
  private readonly ICategoryRepository _categoryRepository;

  public CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository
  )
  {
    _categoryRepository = categoryRepository;
  }

  public async Task<ErrorOr<Category>> Handle(
    CreateCategoryCommand command,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later

    var category = Category.Create(
      command.Name,
      command.Description,
      command.CategoryImageUrl
    );

    _categoryRepository.Add(category);

    return category;
  }
}