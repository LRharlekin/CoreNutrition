using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class User
    {
        public static Error InvalidUserId => Error.Validation(
            code: "User.InvalidId",
            description: "User ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User with given ID does not exist");
            
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in use.");
    }
}