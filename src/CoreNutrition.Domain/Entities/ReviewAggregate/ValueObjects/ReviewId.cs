using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

using ErrorOr;

namespace CoreNutrition.Domain.ReviewAggregate.ValueObjects;

public sealed class ReviewId : AggregateRootId<Guid>
{
  private ReviewId(Guid value) : base(value)
  {
  }

  // CreateUnique() when we need to generate a Guid
  public static ReviewId CreateUnique()
  {
    // TODO: enforce invariants
    return new ReviewId(Guid.NewGuid());
  }

  // Create() when we already have the Guid
  public static ReviewId Create(Guid value)
  {
    // TODO: enforce invariants
    return new ReviewId(value);
  }

  public static ErrorOr<ReviewId> Create(string value)
  {
    if (!Guid.TryParse(value, out var guid))
    {
      return Errors.Review.InvalidReviewId;
    }

    return new ReviewId(guid);
  }
}