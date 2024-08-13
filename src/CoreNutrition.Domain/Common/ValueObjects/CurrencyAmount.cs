using CoreNutrition.Domain.Common.Models;

namespace CoreNutrition.Domain.Common.ValueObjects;

public sealed class CurrencyAmount : ValueObject
{
    public static class Constraints
    {
        public const int CodeLength = 3;
    }

    public decimal Amount { get; private set; }
    public string CurrencyCode { get; private set; }

    private CurrencyAmount(decimal amount, string currencyCode)
    {
        Amount = amount;
        CurrencyCode = currencyCode;
    }

    public static CurrencyAmount CreateNew(decimal amount, string currencyCode)
    {
        return new CurrencyAmount(amount, currencyCode);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return CurrencyCode;
    }

#pragma warning disable CS8618
    private CurrencyAmount()
    {
    }
#pragma warning restore CS8618
}