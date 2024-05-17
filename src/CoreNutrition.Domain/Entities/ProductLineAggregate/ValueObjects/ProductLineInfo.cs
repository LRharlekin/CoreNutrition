using CoreNutrition.Domain.Common.Models;
namespace CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

public sealed class ProductLineInfo : ValueObject
{
    public string DescriptionShort { get; private set; }
    public string DescriptionLong { get; private set; }
    public string SuggestedUse { get; private set; }
    public string Benefit1 { get; private set; }
    public string Benefit2 { get; private set; }
    public string Benefit3 { get; private set; }
    public bool IsMuscleGain { get; private set; }
    public bool IsWeightLoss { get; private set; }
    public bool IsHealthWellness { get; private set; }

    private ProductLineInfo(
        string descriptionShort,
        string descriptionLong,
        string suggestedUse,
        string benefit1,
        string benefit2,
        string benefit3,
        bool isMuscleGain,
        bool isWeightLoss,
        bool isHealthWellness)
    {
        DescriptionShort = descriptionShort;
        DescriptionLong = descriptionLong;
        SuggestedUse = suggestedUse;
        Benefit1 = benefit1;
        Benefit2 = benefit2;
        Benefit3 = benefit3;
        IsMuscleGain = isMuscleGain;
        IsWeightLoss = isWeightLoss;
        IsHealthWellness = isHealthWellness;
    }

    public static ProductLineInfo CreateNew(
        string descriptionShort,
        string descriptionLong,
        string suggestedUse,
        string benefit1,
        string benefit2,
        string benefit3,
        bool isMuscleGain,
        bool isWeightLoss,
        bool isHealthWellness)
    {
        return new ProductLineInfo(
            descriptionShort,
            descriptionLong,
            suggestedUse,
            benefit1,
            benefit2,
            benefit3,
            isMuscleGain,
            isWeightLoss,
            isHealthWellness);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return DescriptionShort;
        yield return DescriptionLong;
        yield return SuggestedUse;
        yield return Benefit1;
        yield return Benefit2;
        yield return Benefit3;
        yield return IsMuscleGain;
        yield return IsWeightLoss;
        yield return IsHealthWellness;
    }

#pragma warning disable CS8618
    private ProductLineInfo()
    {
    }
#pragma warning restore CS8618
}