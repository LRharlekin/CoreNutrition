using ErrorOr;

using CoreNutrition.Domain.CategoryAggregate;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Category
    {
        public static Error InvalidName => Error.Validation(
        code: "Category.InvalidName",
        description: $"Category name must be between {CategoryAggregate.Category.MinNameLength} and {CategoryAggregate.Category.MaxNameLength} characters long.");

        public static Error InvalidDescription => Error.Validation(
        code: "Category.InvalidId",
        description: $"Category description must be between {CategoryAggregate.Category.MinDescriptionLength} and {CategoryAggregate.Category.MaxDescriptionLength} characters long.");

        public static Error InvalidCategoryId => Error.Validation(
        code: "Category.InvalidId",
        description: "Category ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "Category.NotFound",
            description: "Category with given ID does not exist");
        public static Error ListNotFound => Error.NotFound(
            code: "Category.ListNotFound",
            description: "Not able to retrieve a list of categories based on given parameters.");
    }
}