using CoreNutrition.Infrastructure.Common.Persistence;
using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;

namespace CoreNutrition.Infrastructure.Categories.Persistence;

public class CategoryRepository : ICategoryRepository
{
  private static readonly List<Category> _categories = new();
  private readonly CoreNutritionDbContext _dbContext;
  public CategoryRepository(CoreNutritionDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public void Add(Category category)
  {
    _categories.Add(category);
  }
  public async Task AddAsync(Category category)
  {
    _dbContext.Add(category);
    await _dbContext.SaveChangesAsync();
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