using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;

namespace CoreNutrition.Infrastructure.ProductLineSizes.Persistence;

public class ProductLineSizeRepository : IProductLineSizeRepository
{
  private static readonly List<ProductLineSize> _productLineSizes = new();

  public void Add(ProductLineSize productLineSize)
  {
    _productLineSizes.Add(productLineSize);
  }

  public List<ProductLineSize> GetAll()
  {
    return _productLineSizes;
  }

  public ProductLineSize? GetById(ProductLineSizeId productLineSizeId)
  {
    return _productLineSizes.SingleOrDefault(c => c.Id == productLineSizeId);
  }
}