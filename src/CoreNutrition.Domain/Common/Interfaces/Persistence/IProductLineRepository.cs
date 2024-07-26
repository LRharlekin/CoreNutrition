using CoreNutrition.Domain.ProductLineAggregate;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Domain.Common.Interfaces.Persistence;

public interface IProductLineRepository
{
  void Add(ProductLine productLine);

  ProductLine? GetById(ProductLineId productLineId);

  List<ProductLine> GetAll();
}