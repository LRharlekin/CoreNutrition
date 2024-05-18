using CoreNutrition.Domain.CategoryAggregate;

namespace CoreNutrition.Domain.Common.Interfaces.Persistence;

public interface ICategoryRepository
{
  // Category? GetCategoryById(string categoryId);
  void Add(Category category);
}