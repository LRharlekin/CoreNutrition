using ErrorOr;

using CoreNutrition.Domain.Common.ValueObjects;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class AverageRating
    {
        public static Error InvalidDefaultValue(int numRatings, Type valueType, object avgRatingResult) => Error.Validation(
        code: "AverageRating.InvalidDefaultValue",
        description: $"Invalid Average Rating: With {numRatings} ratings or reviews found for this product or product line, the default Average Rating should be NULL, but was returned as type {valueType} with value of {avgRatingResult}.");

        public static Error OutOfRange(decimal? avgRatingResult, int numRatings) => Error.Validation(
            code: "AverageRating.OutOfRange",
            description: $"Value for Average Rating is out of range: Average rating should be between {ValueObjects.AverageRating.MinAvgRating} and {ValueObjects.AverageRating.MaxAvgRating}, but was calculated as {avgRatingResult} on basis of {numRatings} ratings.");
    }
}