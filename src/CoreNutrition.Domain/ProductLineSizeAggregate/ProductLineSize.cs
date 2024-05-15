using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.Events;

using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Domain.ProductLineSizeAggregate;

public sealed class ProductLineSize : AggregateRoot<ProductLineSizeId, Guid>
{
  public SizeId SizeId { get; private set; }
  public ProductLineId ProductLineId { get; private set; }
  public CurrencyAmount RecommendedRetailPrice { get; private set; }

  public DateTime CreatedDateTime { get; private set; }
  public DateTime UpdatedDateTime { get; private set; }

  private ProductLineSize(
    ProductLineSizeId productLineSizeId,
    SizeId sizeId,
    ProductLineId productLineId,
    CurrencyAmount recommendedRetailPrice
    )
    : base(productLineSizeId)
  {
    SizeId = sizeId;
    ProductLineId = productLineId;
    RecommendedRetailPrice = recommendedRetailPrice;
  }

  public static ProductLineSize Create(
    SizeId sizeId,
    ProductLineId productLineId,
    CurrencyAmount recommendedRetailPrice
    )
  {
    var productLineSize = new ProductLineSize(
      ProductLineSizeId.CreateUnique(),
      sizeId,
      productLineId,
      recommendedRetailPrice);

    productLineSize.AddDomainEvent(new ProductLineSizeCreated(productLineSize));

    return productLineSize;
  }
}