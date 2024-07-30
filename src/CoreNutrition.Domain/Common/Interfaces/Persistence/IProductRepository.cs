using CoreNutrition.Domain.ProductAggregate;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;

namespace CoreNutrition.Domain.Common.Interfaces.Persistence;

public interface IProductRepository
{
  void Add(Product product);

  // Product? GetById(ProductId productId);

  // List<Product> GetAll();
}