using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;

namespace CoreNutrition.Infrastructure.ProductLineFlavours.Persistence;

public class ProductLineFlavourRepository : IProductLineFlavourRepository
{
  private static readonly List<ProductLineFlavour> _productLineFlavours = new();

  public void Add(ProductLineFlavour productLineFlavour)
  {
    _productLineFlavours.Add(productLineFlavour);
  }

  public List<ProductLineFlavour> GetAll()
  {
    return _productLineFlavours;
  }

  public ProductLineFlavour? GetById(ProductLineFlavourId productLineFlavourId)
  {
    return _productLineFlavours.SingleOrDefault(c => c.Id == productLineFlavourId);
  }
}