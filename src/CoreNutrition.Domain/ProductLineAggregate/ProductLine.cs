using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineAggregate.Events;

using CoreNutrition.Domain.Common.ValueObjects;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;

namespace CoreNutrition.Domain.ProductLineAggregate;

public sealed class ProductLine : AggregateRoot<ProductLineId, Guid>
{
  private List<ProductId> _productIds = new List<ProductId>();
  private List<ProductLineSizeId> _productLineSizeIds = new List<ProductLineSizeId>();
  private List<ProductLineFlavourId> _productLineFlavourIds = new List<ProductLineFlavourId>();

  public string Name { get; private set; }
  public bool IsPublished { get; private set; }
  public CategoryId CategoryId { get; private set; }
  public AverageRating AverageRating { get; private set; }
  // public ProductLineInfo ProductLineInfo { get; private set; }
  public NutritionFacts NutritionFacts { get; private set; }

  public IReadOnlyList<ProductId> ProductIds => _productIds.AsReadOnly();
  public IReadOnlyList<ProductLineSizeId> ProductLineSizeIds => _productLineSizeIds.AsReadOnly();
  public IReadOnlyList<ProductLineFlavourId> ProductLineFlavourIds => _productLineFlavourIds.AsReadOnly();
  
  public DateTime CreatedDateTime { get; private set; }
  public DateTime UpdatedDateTime { get; private set; }

  // private constructor
  private ProductLine(
    ProductLineId productLineId,
    string name,
    bool isPublished,
    CategoryId categoryId,
    AverageRating averageRating,
    // ProductLineInfo productLineInfo,
    NutritionFacts nutritionFacts,
    DateTime createdDateTime,
    List<ProductId>? productIds,
    List<ProductLineSizeId>? productLineSizeIds,
    List<ProductLineFlavourId>? productLineFlavourIds
    )
    : base(productLineId)
  {
    Name = name;
    IsPublished = isPublished;
    CategoryId = categoryId;
    AverageRating = averageRating;
    // ProductLineInfo = productLineInfo;
    NutritionFacts = nutritionFacts;
    CreatedDateTime = createdDateTime;
    UpdatedDateTime = createdDateTime;
    _productIds = productIds;
    _productLineSizeIds = productLineSizeIds;
    _productLineFlavourIds = productLineFlavourIds;
  }

  // public factory method
  public static ProductLine Create(
    string name,
    bool isPublished,
    CategoryId categoryId,
    AverageRating averageRating,
    // ProductLineInfo productLineInfo,
    NutritionFacts nutritionFacts,
    List<ProductId>? productIds = null,
    List<ProductLineSizeId>? productLineSizeIds = null,
    List<ProductLineFlavourId>? productLineFlavourIds = null
    )
  {
    // TODO: Enforce invariants
    var productLine = new ProductLine(
      ProductLineId.CreateUnique(),
      name,
      isPublished,
      categoryId,
      averageRating,
      // productLineInfo,
      nutritionFacts,
      DateTime.UtcNow,
      productIds ?? new List<ProductId>(),
      productLineSizeIds ?? new List<ProductLineSizeId>(),
      productLineFlavourIds ?? new List<ProductLineFlavourId>()
    );

    productLine.AddDomainEvent(new ProductLineCreated(productLine));

    return productLine;
  }

  // TODO: invoked by relevant domain events
  public void AddProductId(ProductId productId)
  {
    _productIds.Add(productId);
    // UpdatedDateTime = DateTime.UtcNow; // Eventual consitency?
  }

  public void AddProductLineSizeId(ProductLineSizeId productLineSizeId)
  {
    _productLineSizeIds.Add(productLineSizeId);
    // UpdatedDateTime = DateTime.UtcNow; // Eventual consitency?
  }

  public void AddProductLineFlavourId(ProductLineFlavourId productLineFlavourId)
  {
    _productLineFlavourIds.Add(productLineFlavourId);
    // UpdatedDateTime = DateTime.UtcNow; // Eventual consitency?
  }
}