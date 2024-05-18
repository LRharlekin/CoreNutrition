using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.Common.Interfaces.Persistence;

namespace CoreNutrition.Infrastructure.Persistence;

public class CategoryRepository : ICategoryRepository
{
  private static readonly List<Category> _categories = new();

  // public Category? GetCategoryById(string id)
  // {
  // return _categories.SingleOrDefault(u => u.Id == id);
  // }

  public void Add(Category category)
  {
    _categories.Add(category);
  }
}