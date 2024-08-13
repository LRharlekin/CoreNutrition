using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.CategoryAggregate.Events;
using CoreNutrition.Domain.Common.DomainErrors;

using ErrorOr;

namespace CoreNutrition.Domain.CategoryAggregate;

public sealed class Category : AggregateRoot<CategoryId, Guid>
{
  public static class Constraints
  {

    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;
    public const int MinDescriptionLength = 20;
    public const int MaxDescriptionLength = 800;
  }


  private readonly List<ProductLineId> _productLineIds = new();

  public string Name { get; private set; }
  public string Description { get; private set; }
  public Uri CategoryImageUrl { get; private set; }
  public IReadOnlyList<ProductLineId> ProductLineIds => _productLineIds.AsReadOnly();

  public DateTime CreatedDateTime { get; private set; }
  public DateTime UpdatedDateTime { get; private set; }


  private Category(
    CategoryId categoryId,
    string name,
    string description,
    Uri categoryImageUrl,
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

  public static ErrorOr<Category> Create(
    string name,
    string description,
    Uri categoryImageUrl)
  {
    var category = new Category(
      CategoryId.CreateUnique(),
      name,
      description,
      categoryImageUrl,
      DateTime.UtcNow);

    var errors = category.EnforceInvariants();

    if (errors.Count > 0)
    {
      return errors;
    }

    category.AddDomainEvent(new CategoryCreated(category));

    return category;
  }

  // TODO: invoked by ProductLineCreated, ProductLineUpdated, ProductLineDeleted >> event handler in Application Layer
  public void AddProductLineId(ProductLineId productLineId)
  {
    _productLineIds.Add(productLineId);
    // UpdatedDateTime = DateTime.UtcNow; // Eventual consitency?
  }

  private List<Error> EnforceInvariants()
  {
    var errors = new List<Error>();

    if (this.Name.Length < Constraints.MinNameLength || this.Name.Length > Constraints.MaxNameLength)
    {
      errors.Add(Errors.Category.InvalidName);
    }

    if (this.Description.Length < Constraints.MinDescriptionLength || this.Description.Length > Constraints.MaxDescriptionLength)
    {
      errors.Add(Errors.Category.InvalidDescription);
    }

    return errors;
  }

#pragma warning disable CS8618
  private Category()
  {
  }
#pragma warning disable CS8618
}