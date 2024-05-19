using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;

namespace CoreNutrition.Domain.Common.Interfaces.Persistence;

public interface IProductLineFlavourRepository
{
  void Add(ProductLineFlavour productLineFlavour);
  ProductLineFlavour? GetById(ProductLineFlavourId productLineFlavourId);

  List<ProductLineFlavour> GetAll();
}