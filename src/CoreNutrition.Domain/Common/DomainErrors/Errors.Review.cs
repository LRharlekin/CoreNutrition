
using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Review
    {
        public static Error InvalidReviewId => Error.Validation(
            code: "Review.InvalidId",
            description: "Review ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "Review.NotFound",
            description: "Review with given ID does not exist");
    }
}