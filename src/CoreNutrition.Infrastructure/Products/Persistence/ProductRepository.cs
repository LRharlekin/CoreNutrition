using CoreNutrition.Domain.ProductAggregate;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;

namespace CoreNutrition.Infrastructure.Products.Persistence;

public class ProductRepository : IProductRepository
{
  private static readonly List<Product> _products = new();

  public void Add(Product product)
  {
    _products.Add(product);
  }

  public List<Product> GetAll()
  {
    return _products;
  }

  public Product? GetById(ProductId productId)
  {
    return _products.SingleOrDefault(c => c.Id == productId);
  }
}