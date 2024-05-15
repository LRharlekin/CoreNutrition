using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.Events;

using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;

namespace CoreNutrition.Domain.ProductLineSizeAggregate;

public sealed class ProductLineSize : AggregateRoot<ProductLineSizeId, Guid>
{
  private List<ProductId> _productIds = new List<ProductId>();

  public SizeId SizeId { get; private set; }
  public ProductLineId ProductLineId { get; private set; }
  public CurrencyAmount RecommendedRetailPrice { get; private set; }

  public DateTime CreatedDateTime { get; private set; }
  public DateTime UpdatedDateTime { get; private set; }

  public IReadOnlyList<ProductId> ProductIds => _productIds.AsReadOnly();

  private ProductLineSize(
    ProductLineSizeId productLineSizeId,
    SizeId sizeId,
    ProductLineId productLineId,
    CurrencyAmount recommendedRetailPrice,
    DateTime createdDateTime
    )
    : base(productLineSizeId)
  {
    SizeId = sizeId;
    ProductLineId = productLineId;
    RecommendedRetailPrice = recommendedRetailPrice;
    CreatedDateTime = createdDateTime;
    UpdatedDateTime = createdDateTime;
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
      recommendedRetailPrice,
      DateTime.UtcNow);

    productLineSize.AddDomainEvent(new ProductLineSizeCreated(productLineSize));

    return productLineSize;
  }

  // TODO: invoked by relevant domain events
  public void AddProductId(ProductId productId)
  {
    _productIds.Add(productId);
    // UpdatedDateTime = DateTime.UtcNow; // Eventual consitency?
  }
}