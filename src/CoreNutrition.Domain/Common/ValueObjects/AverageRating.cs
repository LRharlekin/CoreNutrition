using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Domain.Common.ValueObjects;

public sealed class AverageRating : ValueObject
{
  public static class Constraints
  {
    public const double MinScore = 1;
    public const double MaxScore = 5;
    public const int MinNumRatings = 0;
  }

  private double? _score;

  private AverageRating(double? score, int numRatings)
  {
    Score = score;
    NumRatings = numRatings;
  }

  public double? Score
  {
    get => NumRatings > 0 ? _score : null;
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

  public static ErrorOr<AverageRating> CreateNew(double? rating = null, int numRatings = 0)
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

    if (this.NumRatings <= Constraints.MinNumRatings && this.Score.HasValue)
    {
      errors.Add(Errors.AverageRating.InvalidDefaultValue(this.NumRatings, this.Score.GetType(), this.Score));
    }

    if (this.NumRatings > Constraints.MinNumRatings && this.Score is null)
    {
      errors.Add(Errors.AverageRating.ScoreIsNull(this.NumRatings));
    }

    if (this.NumRatings > Constraints.MinNumRatings && this.Score.HasValue && (this.Score < Constraints.MinScore || this.Score > Constraints.MaxScore))
    {
      errors.Add(Errors.AverageRating.OutOfRange(this.Score!, this.NumRatings));
    }

    return errors;
  }
}