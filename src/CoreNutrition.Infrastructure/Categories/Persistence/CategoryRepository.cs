using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;

namespace CoreNutrition.Infrastructure.Categories.Persistence;

public class CategoryRepository : ICategoryRepository
{
  private static readonly List<Category> _categories = new();

  public void Add(Category category)
  {
    _categories.Add(category);
  }

  // update

  // delete

  public Category? GetById(CategoryId categoryId)
  {
    return _categories.SingleOrDefault(c => c.Id == categoryId);
  }

  public List<Category> GetAll()
  {
    return _categories;
  }
}