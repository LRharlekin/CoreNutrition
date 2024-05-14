using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Category
    {
        public static Error InvalidCategoryId => Error.Validation(
        code: "Category.InvalidId",
        description: "Category ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "Category.NotFound",
            description: "Category with given ID does not exist");
    }
}