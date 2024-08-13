using CoreNutrition.Domain.ProductAggregate;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Infrastructure.Common.Persistence;


namespace CoreNutrition.Infrastructure.Products.Persistence;

public class ProductRepository : IProductRepository
{
  private static readonly List<Product> _products = new();
  private readonly CoreNutritionDbContext _dbContext;
  public ProductRepository(CoreNutritionDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public void Add(Product product)
  {
    _products.Add(product);
    // _dbContext.Products.Add(product);
  }

  // public List<Product> GetAll()
  // {
  // return _products;
  //   return _dbContext.Products;
  // }

  // public Product? GetById(ProductId productId)
  // {
  // return _products.SingleOrDefault(p => p.Id == productId);
  // }
}