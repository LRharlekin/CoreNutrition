using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.ValueObjects;
using CoreNutrition.Domain.Common.DomainErrors;

using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.Entities;
using CoreNutrition.Domain.ProductLineSizeAggregate.Events;

using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;

namespace CoreNutrition.Domain.ProductLineSizeAggregate;

public sealed class ProductLineSize : AggregateRoot<ProductLineSizeId, Guid>
{
  // invariant constants:
  public const decimal MinRRP = 0;

  private List<ProductId> _productIds = new List<ProductId>();
  public IReadOnlyList<ProductId> ProductIds => _productIds.AsReadOnly();

  public ProductLineId ProductLineId { get; private set; }
  public CurrencyAmount RecommendedRetailPrice { get; private set; }
  public SizeVariant SizeVariant { get; private set; }

  public DateTime CreatedDateTime { get; private set; }
  public DateTime UpdatedDateTime { get; private set; }


  private ProductLineSize(
    ProductLineSizeId productLineSizeId,
    ProductLineId productLineId,
    CurrencyAmount recommendedRetailPrice,
    SizeVariant sizeVariant,
    DateTime createdDateTime
    )
    : base(productLineSizeId)
  {
    ProductLineId = productLineId;
    SizeVariant = sizeVariant;
    RecommendedRetailPrice = recommendedRetailPrice;
    CreatedDateTime = createdDateTime;
    UpdatedDateTime = createdDateTime;
  }

  public static ErrorOr<ProductLineSize> Create(
    ProductLineId productLineId, // FK
    CurrencyAmount recommendedRetailPrice,
    SizeVariant sizeVariant // contained Entity
    )
  {
    var productLineSize = new ProductLineSize(
      ProductLineSizeId.CreateUnique(),
      productLineId, // FK
      recommendedRetailPrice, // VO
      sizeVariant, // contained Entity
      DateTime.UtcNow);

    var errors = productLineSize.EnforceInvariants();

    if (errors.Count > 0)
    {
      return errors;
    }

    productLineSize.AddDomainEvent(new ProductLineSizeCreated(productLineSize));

    return productLineSize;
  }

  // TODO: invoked by relevant domain events, e.g. when a Product is updated/created
  public void AddProductId(ProductId productId)
  {
    _productIds.Add(productId);
    // UpdatedDateTime = DateTime.UtcNow; // Eventual consitency?
  }

  private List<Error> EnforceInvariants()
  {
    var errors = new List<Error>();

    if (RecommendedRetailPrice.Amount <= MinRRP)
    {
      errors.Add(Errors.ProductLineSize.InvalidRecommendedRetailPrice);
    }

    return errors;
  }
}