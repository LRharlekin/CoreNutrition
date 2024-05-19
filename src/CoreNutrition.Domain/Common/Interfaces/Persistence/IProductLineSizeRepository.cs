using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;

namespace CoreNutrition.Domain.Common.Interfaces.Persistence;

public interface IProductLineSizeRepository
{
  void Add(ProductLineSize productLineSize);
  ProductLineSize? GetById(ProductLineSizeId productLineSizeId);

  List<ProductLineSize> GetAll();
}