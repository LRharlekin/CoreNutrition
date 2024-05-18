using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;

namespace CoreNutrition.Domain.Common.Interfaces.Persistence;

public interface ICategoryRepository
{
  // Category? GetCategoryById(string categoryId);
  void Add(Category category);
  Category? GetById(CategoryId categoryId);
}