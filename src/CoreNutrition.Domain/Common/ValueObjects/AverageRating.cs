using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Domain.Common.ValueObjects;

public sealed class AverageRating : ValueObject
{
  // invariant constants:
  public const decimal MinAvgRating = 1;
  public const decimal MaxAvgRating = 5;

  private decimal _value;

  private AverageRating(decimal value, int numRatings)
  {
    Value = value;
    NumRatings = numRatings;
  }

  public decimal? Value { get => NumRatings > 0 ? _value : null; private set => _value = value!.Value; }
  public int NumRatings { get; private set; }

  public static ErrorOr<AverageRating> CreateNew(decimal rating = 0, int numRatings = 0)
  {
    var averageRating = new AverageRating(rating, numRatings);

    var errors = averageRating.EnforceInvariants();

    if (errors.Count > 0)
    {
      return errors;
    }

    return averageRating;
  }


  /* TODO: Trigger with ReviewCreated / ReviewRemoved domain events */
  // public void AddNewRating(Rating rating)
  // {
  //   Value = ((Value * NumRatings) + rating.Value) / ++NumRatings;
  // }

  // public void RemoveRating(Rating rating)
  // {
  //   Value = ((Value * NumRatings) - rating.Value) / --NumRatings;
  // }

  public override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Value;
  }

#pragma warning disable CS8618
  private AverageRating()
  {
  }
#pragma warning restore CS8618

  private List<Error> EnforceInvariants()
  {
    var errors = new List<Error>();

    if (this.NumRatings <= 0 && this.Value is not null)
    {
      errors.Add(Errors.AverageRating.InvalidDefaultValue(this.NumRatings, this.Value.GetType(), this.Value));
    }

    if (this.NumRatings > 0 && this.Value is not null && this.Value is not >= MinAvgRating or <= MaxAvgRating)
    {
      errors.Add(Errors.AverageRating.OutOfRange(this.Value!, this.NumRatings));
    }

    return errors;
  }
}