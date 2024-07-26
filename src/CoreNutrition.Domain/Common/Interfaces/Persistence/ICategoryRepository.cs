using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;

namespace CoreNutrition.Domain.Common.Interfaces.Persistence;

public interface ICategoryRepository
{
  void Add(Category category);

  Category? GetById(CategoryId categoryId);

  List<Category> GetAll();
}