using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;

namespace CoreNutrition.Domain.Common.Interfaces.Persistence;

public interface ICategoryRepository
{
  void Add(Category category);

  // Task AddAsync(Category category);
  Category? GetById(CategoryId categoryId);

  // Task<Category?> GetByIdAsync(CategoryId categoryId);
  List<Category> GetAll();
  // Task<List<Category>> ListAsync(HostId hostId);
  // Task UpdateAsync(Category category);
  // Task<bool> ExistsAsync(CategoryId categoryId);
}