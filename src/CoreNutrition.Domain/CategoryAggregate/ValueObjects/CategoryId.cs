using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

using ErrorOr;

namespace CoreNutrition.Domain.CategoryAggregate.ValueObjects;

public sealed class CategoryId : AggregateRootId<Guid>
{
  private CategoryId(Guid value) : base(value)
  {
  }

  public static CategoryId CreateUnique()
  {
    // TODO: enforce invariants
    return new CategoryId(Guid.NewGuid());
  }

  public static CategoryId Create(Guid value)
  {
    // TODO: enforce invariants
    return new CategoryId(value);
  }

  public static ErrorOr<CategoryId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.Category.InvalidCategoryId;
    }

    return new CategoryId(guid);
  }
}