using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineFlavourAggregate.Events;

using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;

namespace CoreNutrition.Domain.ProductLineFlavourAggregate;

public sealed class ProductLineFlavour : AggregateRoot<ProductLineFlavourId, Guid>
{
  private List<ProductId> _productIds = new List<ProductId>();

  public string Flavour { get; private set; }
  public ProductLineId ProductLineId { get; private set; }
  public string FlavourImageUrl { get; private set; }
  public IReadOnlyList<ProductId> ProductIds => _productIds.AsReadOnly();  

  public DateTime CreatedDateTime { get; private set; }
  public DateTime UpdatedDateTime { get; private set; }

  private ProductLineFlavour(
    ProductLineFlavourId productLineFlavourId,
    string flavour,
    ProductLineId productLineId,
    string flavourImageUrl,
    DateTime createdDateTime
    )
    : base(productLineFlavourId)
  {
    Flavour = flavour;
    ProductLineId = productLineId;
    FlavourImageUrl = flavourImageUrl;
    CreatedDateTime = createdDateTime;
    UpdatedDateTime = createdDateTime;
  }

  public static ProductLineFlavour Create(
    string flavour,
    ProductLineId productLineId,
    string flavourImageUrl
    )
  {
    // TODO: Enforce invariants
    var productLineFlavour = new ProductLineFlavour(
      ProductLineFlavourId.CreateUnique(),
      flavour,
      productLineId,
      flavourImageUrl,
      DateTime.UtcNow
    );

    productLineFlavour.AddDomainEvent(new ProductLineFlavourCreated(productLineFlavour));

    return productLineFlavour;
  }

  // TODO: invoked by relevant domain events, e.g. ProductCreated, ProductDeleted, ProductUpdated
  public void AddProductId(ProductId productId)
  {
    _productIds.Add(productId);
    // UpdatedDateTime = DateTime.UtcNow; // Eventual consitency?
  }
}