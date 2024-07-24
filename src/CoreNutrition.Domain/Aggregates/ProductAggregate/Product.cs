using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.ValueObjects;
using CoreNutrition.Domain.Common.DomainErrors;

using CoreNutrition.Domain.ProductAggregate.ValueObjects;
using CoreNutrition.Domain.ProductAggregate.Events;

using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;
using CoreNutrition.Domain.ReviewAggregate.ValueObjects;
using CoreNutrition.Domain.ShopOrderAggregate.ValueObjects;
using CoreNutrition.Domain.CartAggregate.ValueObjects;

namespace CoreNutrition.Domain.ProductAggregate;

public sealed class Product : AggregateRoot<ProductId, Guid>
{
  // invariant constants
  public static class Constraints
  {
    public const int MinNameLength = 3;
    public const int MaxNameLength = 100;
    public const decimal MinRetailPrice = 0;
    public const int MinQuantityInStock = 0;
  }

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

  private List<ReviewId> _reviewIds = new List<ReviewId>();
  private List<OrderLineItemId> _orderLineItemIds = new List<OrderLineItemId>();
  private List<CartItemId> _cartItemIds = new List<CartItemId>();
  public IReadOnlyList<ReviewId> ReviewIds => _reviewIds.AsReadOnly();
  public IReadOnlyList<OrderLineItemId> OrderLineItemIds => _orderLineItemIds.AsReadOnly();
  public IReadOnlyList<CartItemId> CartItemIds => _cartItemIds.AsReadOnly();

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
    DateTime createdDateTime,
    List<ReviewId>? reviewIds,
    List<OrderLineItemId>? orderLineItemIds,
    List<CartItemId>? cartItemIds
  )
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
    _reviewIds = reviewIds;
    _orderLineItemIds = orderLineItemIds;
    _cartItemIds = cartItemIds;
  }

  public static ErrorOr<Product> Create(
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
    List<ReviewId>? reviewIds = null,
    List<OrderLineItemId>? orderLineItemIds = null,
    List<CartItemId>? cartItemIds = null
    )
  {
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
      DateTime.UtcNow,
      reviewIds ?? new List<ReviewId>(),
      orderLineItemIds ?? new List<OrderLineItemId>(),
      cartItemIds ?? new List<CartItemId>()
    );

    var errors = product.EnforceInvariants();

    if (errors.Count > 0)
    {
      return errors;
    }

    product.AddDomainEvent(new ProductCreated(product));

    return product;
  }

  // TODO: invoked by relevant domain events, 
  // event: ReviewCreated
  public void AddReviewToProduct(ReviewId reviewId)
  // public void AddReviewId(ReviewId reviewId)
  {
    _reviewIds.Add(reviewId);
    // UpdatedDateTime = DateTime.UtcNow; // Eventual consitency?
  }

  // event ProductAddedToCart
  // event ProductRemovedFromCart
  // event OrderCancelled

  private List<Error> EnforceInvariants()
  {
    var errors = new List<Error>();

    if (this.Name.Length is < Constraints.MinNameLength or > Constraints.MaxNameLength)
    {
      errors.Add(Errors.Product.InvalidNameLength);
    }

    if (this.RetailPrice.Amount <= Constraints.MinRetailPrice)
    {
      errors.Add(Errors.Product.InvalidRetailPrice);
    }

    if (this.QuantityInStock < Constraints.MinQuantityInStock)
    {
      errors.Add(Errors.Product.InvalidQuantityInStock);
    }

    return errors;
  }
}
