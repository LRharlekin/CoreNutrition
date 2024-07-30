using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.Events;

namespace CoreNutrition.Domain.ProductLineSizeAggregate.Entities;

public sealed class SizeVariant : Entity<SizeVariantId>
{
  // invariant constants:
  public static class Constraints
  {
    public const int MinNameLength = 1;
    public const int MaxNameLength = 20;
    public const int MinUnits = 1;
    public const int MinUnitWeightInGrams = 1;
    public const int MinUnitVolumeInMilliliters = 1;
  }

  private List<ProductLineSizeId> _productLineSizeIds = new();
  public IReadOnlyList<ProductLineSizeId> ProductLineSizeIds => _productLineSizeIds.AsReadOnly();  // related / referenced entities

  public string Name { get; private set; }
  public int? UnitWeightInGrams { get; private set; }
  public int? UnitVolumeInMilliliters { get; private set; }
  public int Units { get; private set; }
  public SizeVariantId? SingleSizeVariantId { get; private set; }

  private SizeVariant(
    string name,
    int units,
    int? unitWeightInGrams = null,
    int? unitVolumeInMilliliters = null,
    SizeVariantId? singleSizeVariantId = null,
    SizeVariantId? id = null)
  // : base(sizeVariantId)
  : base(id ?? SizeVariantId.CreateUnique())
  {
    Name = name;
    Units = units;
    UnitWeightInGrams = unitWeightInGrams;
    UnitVolumeInMilliliters = unitVolumeInMilliliters;
    SingleSizeVariantId = singleSizeVariantId;
  }

  public static ErrorOr<SizeVariant> Create(
    string name,
    int units,
    int? unitWeightInGrams = null,
    int? unitVolumeInMilliliters = null,
    SizeVariantId? singleSizeVariantId = null)
  {
    var sizeVariant = new SizeVariant(
      name,
      units,
      unitWeightInGrams,
      // unitWeightInGrams ?? new(),
      unitVolumeInMilliliters,
      // unitVolumeInMilliliters ?? new(),
      singleSizeVariantId);

    var errors = sizeVariant.EnforceInvariants();

    if (errors.Count > 0)
    {
      return errors;
    }

    // emit domain events
    sizeVariant.AddDomainEvent(new SizeVariantCreated(sizeVariant));

    return sizeVariant;
  }

  // TODO: invoked by relevant domain events, e.g. ProductLineSizeCreated, ProductLineSizeUpdated, ProductLineSizeDeleted
  public void AddProductLineSizeId(ProductLineSizeId productLineSizeId)
  {
    _productLineSizeIds.Add(productLineSizeId);
  }

  private List<Error> EnforceInvariants()
  {
    var errors = new List<Error>();

    if (this.Name.Length is < Constraints.MinNameLength or > Constraints.MaxNameLength)
    {
      errors.Add(Errors.SizeVariant.InvalidName);
    }

    if (this.Units < Constraints.MinUnits)
    {
      errors.Add(Errors.SizeVariant.InvalidUnits);
    }

    if (
      this.Units > 1
      && this.SingleSizeVariantId is null)
    {
      errors.Add(Errors.SizeVariant.MissingSingleSizeReference);
    }

    if (
      this.UnitWeightInGrams is null
      && this.UnitVolumeInMilliliters is null)
    {
      errors.Add(Errors.SizeVariant.MissingWeightOrVolume);
    }

    if (
      this.UnitWeightInGrams.HasValue
      && this.UnitVolumeInMilliliters.HasValue)
    {
      Console.WriteLine("ERROR: Weight and Volume not allowed");
      Console.WriteLine("GRAMS:");
      Console.WriteLine(this.UnitWeightInGrams.ToString());
      Console.WriteLine("MILLILITERS:");
      Console.WriteLine(this.UnitVolumeInMilliliters.ToString());
      errors.Add(Errors.SizeVariant.WeightAndVolumeNotAllowed);
    }

    if (
      (this.UnitWeightInGrams.HasValue && this.UnitWeightInGrams < Constraints.MinUnitWeightInGrams)
      || (this.UnitVolumeInMilliliters.HasValue && this.UnitVolumeInMilliliters < Constraints.MinUnitVolumeInMilliliters))
    {
      Console.WriteLine("ERROR: Invalid Weight or Volume");
      Console.WriteLine("GRAMS:");
      Console.WriteLine(this.UnitWeightInGrams.ToString());
      Console.WriteLine("MILLILITERS:");
      Console.WriteLine(this.UnitVolumeInMilliliters.ToString());
      errors.Add(Errors.SizeVariant.InvalidWeightOrVolume);
    }

    return errors;
  }

#pragma warning disable CS8618
  private SizeVariant()
  {
  }
#pragma warning restore CS8618
}