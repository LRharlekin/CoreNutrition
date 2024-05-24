using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

public sealed class ProductLineInfo : ValueObject
{
    // invariant constants
    public const int MinDescriptionShortLength = 50;
    public const int MaxDescriptionShortLength = 200;
    public const int MinDescriptionLongLength = 200;
    public const int MaxDescriptionLongLength = 1000;
    public const int MinSuggestedUseLength = 20;
    public const int MaxSuggestedUseLength = 400;
    public const int MaxBenefitLength = 150;

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

    public static ErrorOr<ProductLineInfo> CreateNew(
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
        var productLineInfo = new ProductLineInfo(
            descriptionShort,
            descriptionLong,
            suggestedUse,
            benefit1,
            benefit2,
            benefit3,
            isMuscleGain,
            isWeightLoss,
            isHealthWellness);

        var errors = productLineInfo.EnforceInvariants();

        if (errors.Count > 0)
        {
            return errors;
        }

        return productLineInfo;
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

    private List<Error> EnforceInvariants()
    {
        var errors = new List<Error>();

        if (this.DescriptionShort.Length is < MinDescriptionShortLength or > MaxDescriptionShortLength)
        {
            errors.Add(Errors.ProductLineInfo.InvalidShortDescription);
        }

        if (this.DescriptionLong.Length is < MinDescriptionLongLength or > MaxDescriptionLongLength)
        {
            errors.Add(Errors.ProductLineInfo.InvalidLongDescription);
        }

        if (this.SuggestedUse.Length is < MinSuggestedUseLength or > MaxSuggestedUseLength)
        {
            errors.Add(Errors.ProductLineInfo.InvalidSuggestedUse);
        }

        if (
            this.Benefit1.Length > MaxBenefitLength ||
            this.Benefit2.Length > MaxBenefitLength ||
            this.Benefit3.Length > MaxBenefitLength)
        {
            errors.Add(Errors.ProductLineInfo.InvalidBenefitLength);
        }

        if (
            this.Benefit1.Equals(this.Benefit2, StringComparison.OrdinalIgnoreCase) ||
            this.Benefit1.Equals(this.Benefit3, StringComparison.OrdinalIgnoreCase) ||
            this.Benefit2.Equals(this.Benefit3, StringComparison.OrdinalIgnoreCase)
        )
        {
            errors.Add(Errors.ProductLineInfo.DuplicateBenefits);
        }

        if (!this.IsMuscleGain && !this.IsWeightLoss && !this.IsHealthWellness)
        {
            errors.Add(Errors.ProductLineInfo.DietGoalRequired);
        }

        return errors;
    }
}