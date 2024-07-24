using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineFlavourAggregate.Events;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;

namespace CoreNutrition.Domain.ProductLineFlavourAggregate;

public sealed class ProductLineFlavour : AggregateRoot<ProductLineFlavourId, Guid>
{
  public const int MinNameLength = 3;
  public const int MaxNameLength = 40;

  private List<ProductId> _productIds = new List<ProductId>();

  public ProductLineId ProductLineId { get; private set; }
  public string Flavour { get; private set; }
  public Uri FlavourImageUrl { get; private set; }
  public IReadOnlyList<ProductId> ProductIds => _productIds.AsReadOnly();

  public DateTime CreatedDateTime { get; private set; }
  public DateTime UpdatedDateTime { get; private set; }

  private ProductLineFlavour(
    ProductLineFlavourId productLineFlavourId,
    string flavour,
    ProductLineId productLineId,
    Uri flavourImageUrl,
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
  public static ErrorOr<ProductLineFlavour> Create(
    string flavour,
    ProductLineId productLineId,
    Uri flavourImageUrl
    )
  {

    var productLineFlavour = new ProductLineFlavour(
      ProductLineFlavourId.CreateUnique(),
      flavour,
      productLineId,
      flavourImageUrl,
      DateTime.UtcNow
    );

    var errors = productLineFlavour.EnforceInvariants();

    if (errors.Count > 0)
    {
      return errors;
    }

    productLineFlavour.AddDomainEvent(new ProductLineFlavourCreated(productLineFlavour));

    return productLineFlavour;
  }

  // TODO: invoked by relevant domain events, e.g. ProductCreated, ProductDeleted, ProductUpdated
  public void AddProductId(ProductId productId)
  {
    _productIds.Add(productId);
    // UpdatedDateTime = DateTime.UtcNow; // Eventual consitency?
  }

  private List<Error> EnforceInvariants()
  {
    List<Error> errors = new();

    if (this.Flavour.Length < MinNameLength || this.Flavour.Length > MaxNameLength)
    {
      errors.Add(Errors.ProductLineFlavour.InvalidName);
    }

    return errors;
  }
}