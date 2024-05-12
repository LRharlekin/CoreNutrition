using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Customer
    {
        public static Error InvalidCustomerId => Error.Validation(
        code: "Customer.InvalidId",
        description: "Customer ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "Customer.NotFound",
            description: "Customer with given ID does not exist");
    }
}