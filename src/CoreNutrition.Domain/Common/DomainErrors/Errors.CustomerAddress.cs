using ErrorOr;

namespace CoreNutrition.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class CustomerAddress
    {
        public static Error InvalidCustomerAddressId => Error.Validation(
            code: "CustomerAddress.InvalidId",
            description: "Customer Address ID is invalid");

        public static Error NotFound => Error.NotFound(
            code: "CustomerAddress.NotFound",
            description: "Customer Address with given ID does not exist");
    }
}