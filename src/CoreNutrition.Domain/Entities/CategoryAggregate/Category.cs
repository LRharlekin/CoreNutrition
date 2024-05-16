using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.CategoryAggregate.Events;

namespace CoreNutrition.Domain.CategoryAggregate;

public sealed class Category : AggregateRoot<CategoryId, Guid>
{
  private List<ProductLineId> _productLineIds = new();

  public string Name { get; private set; }
  public string Description { get; private set; }
  public string CategoryImageUrl { get; private set; }
  public IReadOnlyList<ProductLineId> ProductLineIds => _productLineIds.AsReadOnly();

  public DateTime CreatedDateTime { get; private set; }
  public DateTime UpdatedDateTime { get; private set; }


  private Category(
    CategoryId categoryId,
    string name,
    string description,
    string categoryImageUrl,
    DateTime createdDateTime
    )
    : base(categoryId)
  {
    Name = name;
    Description = description;
    CategoryImageUrl = categoryImageUrl;
    CreatedDateTime = createdDateTime;
    UpdatedDateTime = createdDateTime;
  }

  public static Category Create(
    string name,
    string description,
    string categoryImageUrl)
  {
    // TODO: enforce invariants
    var category = new Category(
      CategoryId.CreateUnique(),
      name,
      description,
      categoryImageUrl,
      DateTime.UtcNow);

    category.AddDomainEvent(new CategoryCreated(category));

    return category;
  }

  // TODO: invoked by ProductLineCreated, ProductLineUpdated, ProductLineDeleted
  public void AddProductLineId(ProductLineId productLineId)
  {
    _productLineIds.Add(productLineId);
    // UpdatedDateTime = DateTime.UtcNow; // Eventual consitency?
  }

  #pragma warning disable CS8618
  private Category()
  {
  }
  #pragma warning disable CS8618
}