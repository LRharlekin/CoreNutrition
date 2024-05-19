using CoreNutrition.Domain.ProductLineAggregate;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;

namespace CoreNutrition.Infrastructure.ProductLines.Persistence;

public class ProductLineRepository : IProductLineRepository
{
  private static readonly List<ProductLine> _productLines = new();

  public void Add(ProductLine productLine)
  {
    _productLines.Add(productLine);
  }

  public List<ProductLine> GetAll()
  {
    return _productLines;
  }

  public ProductLine? GetById(ProductLineId productLineId)
  {
    return _productLines.SingleOrDefault(c => c.Id == productLineId);
  }
}