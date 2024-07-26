using ErrorOr;

using CoreNutrition.Domain.Common.ValueObjects;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class AverageRating
    {
        public static Error InvalidDefaultValue(int numRatings, Type scoreValueType, object avgRatingResult) => Error.Validation(
            code: "AverageRating.InvalidDefaultValue",
            description: $"Invalid Average Rating: With {numRatings} ratings or reviews found for this entity, the default Average Rating Score should be NULL, but was returned as type {scoreValueType} with value of {avgRatingResult}.");

        public static Error ScoreIsNull(int numRatings) => Error.Validation(
            code: "AverageRating.ScoreIsNull",
            description: $"Average Rating Score is NULL: With {numRatings} ratings or reviews found for this entity, the Average Rating Score should be a decimal number between {ValueObjects.AverageRating.Constraints.MinScore} and {ValueObjects.AverageRating.Constraints.MaxScore}, but was returned with value of NULL.");


        public static Error OutOfRange(decimal? avgRatingResult, int numRatings) => Error.Validation(
            code: "AverageRating.OutOfRange",
            description: $"Value for Average Rating is out of range: Average Rating Score should be between {ValueObjects.AverageRating.Constraints.MinScore} and {ValueObjects.AverageRating.Constraints.MaxScore}, but was calculated as {avgRatingResult} on basis of {numRatings} ratings.");
    }
}