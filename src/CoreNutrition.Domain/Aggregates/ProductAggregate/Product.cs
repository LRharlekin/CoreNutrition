using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.ValueObjects;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;
using CoreNutrition.Domain.ProductAggregate.Events;

using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;
using CoreNutrition.Domain.ReviewAggregate.ValueObjects;

namespace CoreNutrition.Domain.ProductAggregate;

public sealed class Product : AggregateRoot<ProductId, Guid>
{

  private List<ReviewId> _reviewIds = new List<ReviewId>();
  
  public string Name { get; private set; }
  public bool IsPublished { get; private set; }
  public AverageRating AverageRating { get; private set; }
  public CurrencyAmount RetailPrice { get; private set; }
  public int QuantityInStock { get; private set; }
  public ProductLineId ProductLineId { get; private set; }
  public ProductLineSizeId ProductLineSizeId { get; private set; }
  public ProductLineFlavourId ProductLineFlavourId { get; private set; }
  public bool IsVegan { get; private set; }
  public bool IsSample { get; private set; }
  public string ProductImageUrl { get; private set; }
  public IReadOnlyList<ReviewId> ReviewIds => _reviewIds.AsReadOnly();

  public DateTime CreatedDateTime { get; private set; }
  public DateTime UpdatedDateTime { get; private set; }

  private Product(
    ProductId productId,
    string name,
    bool isPublished,
    AverageRating averageRating,
    CurrencyAmount retailPrice,
    int quantityInStock,
    ProductLineId productLineId,
    ProductLineSizeId productLineSizeId,
    ProductLineFlavourId productLineFlavourId,
    bool isVegan,
    bool isSample,
    string productImageUrl,
    DateTime createdDateTime)
    : base(productId)
  {
    Name = name;
    IsPublished = isPublished;
    AverageRating = averageRating;
    RetailPrice = retailPrice;
    QuantityInStock = quantityInStock;
    ProductLineId = productLineId;
    ProductLineSizeId = productLineSizeId;
    ProductLineFlavourId = productLineFlavourId;
    IsVegan = isVegan;
    IsSample = isSample;
    ProductImageUrl = productImageUrl;
    CreatedDateTime = createdDateTime;
    UpdatedDateTime = createdDateTime;
  }

  public static Product Create(
    string name,
    bool isPublished,
    AverageRating averageRating,
    CurrencyAmount retailPrice,
    int quantityInStock,
    ProductLineId productLineId,
    ProductLineSizeId productLineSizeId,
    ProductLineFlavourId productLineFlavourId,
    bool isVegan,
    bool isSample,
    string productImageUrl)
  {
    // TODO: Enforce invariants
    var product = new Product(
      ProductId.CreateUnique(),
      name,
      isPublished,
      averageRating,
      retailPrice,
      quantityInStock,
      productLineId,
      productLineSizeId,
      productLineFlavourId,
      isVegan,
      isSample,
      productImageUrl,
      DateTime.UtcNow
    );

    product.AddDomainEvent(new ProductCreated(product));

    return product;
  }

  // TODO: invoked by relevant domain events, ReviewCreated
  public void AddReviewId(ReviewId reviewId)
  {
    _reviewIds.Add(reviewId);
    // UpdatedDateTime = DateTime.UtcNow; // Eventual consitency?
  }
}