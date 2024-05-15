using CoreNutrition.Domain.Common.Models;
namespace CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

public sealed class ProductLineinfo : ValueObject
{
    public decimal Amount { get; private set; }
    public string CurrencyCode { get; private set; }

    private ProductLineinfo(decimal amount, string currencyCode)
    {
        Amount = amount;
        CurrencyCode = currencyCode;
    }

    public static ProductLineinfo CreateNew(decimal amount, string currencyCode)
    {
        return new ProductLineinfo(amount, currencyCode);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return CurrencyCode;
    }

#pragma warning disable CS8618
    private ProductLineinfo()
    {
    }
#pragma warning restore CS8618
}