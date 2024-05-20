using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.ValueObjects;

using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.Entities;
using CoreNutrition.Domain.ProductLineSizeAggregate.Events;

using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;

namespace CoreNutrition.Domain.ProductLineSizeAggregate;

public sealed class ProductLineSize : AggregateRoot<ProductLineSizeId, Guid>
{
  private List<ProductId> _productIds = new List<ProductId>();

  public ProductLineId ProductLineId { get; private set; }
  public CurrencyAmount RecommendedRetailPrice { get; private set; }
  public SizeVariant SizeVariant { get; private set; }

  public DateTime CreatedDateTime { get; private set; }
  public DateTime UpdatedDateTime { get; private set; }

  public IReadOnlyList<ProductId> ProductIds => _productIds.AsReadOnly();

  private ProductLineSize(
    ProductLineSizeId productLineSizeId, // PK
    ProductLineId productLineId, // FK
    SizeVariant sizeVariant, // contained Entity
    CurrencyAmount recommendedRetailPrice, // VO
    DateTime createdDateTime
    )
    : base(productLineSizeId) // PK
  {
    ProductLineId = productLineId; // FK
    SizeVariant = sizeVariant; // contained Entity
    RecommendedRetailPrice = recommendedRetailPrice; // VO
    CreatedDateTime = createdDateTime;
    UpdatedDateTime = createdDateTime;
  }

  public static ProductLineSize Create(
    ProductLineId productLineId, // FK
    SizeVariant sizeVariant, // contained Entity
    CurrencyAmount recommendedRetailPrice
    )
  {
    var productLineSize = new ProductLineSize(
      ProductLineSizeId.CreateUnique(),
      productLineId, // FK
      sizeVariant, // contained Entity
      CurrencyAmount.CreateNew(), // VO
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