using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Domain.Common.ValueObjects;

public sealed class AverageRating : ValueObject
{
  public static class Constraints
  {
    public const decimal MinAvgRating = 1;
    public const decimal MaxAvgRating = 5;
  }

  private decimal? _score;

  private AverageRating(decimal? score, int numRatings)
  {
    Score = score;
    NumRatings = numRatings;
  }

  public decimal? Score
  {
    get => NumRatings > 0 ? _score : null;
    // private set => _score = value!.Value; }
    private set
    {
      if (value.HasValue)
      {
        _score = value.Value;
      }
      else
      {
        _score = null;
      }
    }
  }
  public int NumRatings { get; private set; }

  public static ErrorOr<AverageRating> CreateNew(decimal? rating = null, int numRatings = 0)
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
    yield return Score;
  }

#pragma warning disable CS8618
  private AverageRating()
  {
  }
#pragma warning restore CS8618

  private List<Error> EnforceInvariants()
  {
    var errors = new List<Error>();

    if (this.NumRatings <= 0 && this.Score is not null)
    {
      errors.Add(Errors.AverageRating.InvalidDefaultValue(this.NumRatings, this.Score.GetType(), this.Score));
    }

    if (this.NumRatings > 0 && this.Score is not null && (this.Score < Constraints.MinAvgRating || this.Score > Constraints.MaxAvgRating))
    {
      errors.Add(Errors.AverageRating.OutOfRange(this.Score!, this.NumRatings));
    }

    return errors;
  }
}