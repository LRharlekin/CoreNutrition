using ErrorOr;

using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;

namespace CoreNutrition.Domain.ProductLineSizeAggregate.Entities;

public sealed class SizeVariant : Entity<SizeVariantId>
{
  // invariants:
  public const int MinNameLength = 1;
  public const int MaxNameLength = 20;
  public const int MinUnits = 1;
  public const int MinUnitWeightInGrams = 1;
  public const int MinUnitVolumeInMilliliters = 1;

  private List<ProductLineSizeId> _productLineSizeIds = new();

  public string Name { get; private set; }
  public int? UnitWeightInGrams { get; private set; }
  public int? UnitVolumeInMilliliters { get; private set; }
  public int Units { get; private set; }
  public SizeVariantId? SingleSizeVariantId { get; private set; }
  public IReadOnlyList<ProductLineSizeId> ProductLineSizeIds => _productLineSizeIds.AsReadOnly();  // related / referenced entities


  private SizeVariant(
    string name,
    int units,
    int? unitWeightInGrams = null,
    int? unitVolumeInMilliliters = null,
    SizeVariantId? singleSizeVariantId = null,
    SizeVariantId? id = null)
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
    // enforce invariants
    List<Error> errors = [];

    if (name.Length is < MinNameLength or > MaxNameLength)
    {
      errors.Add(Errors.SizeVariant.InvalidName);
    }

    if (units < MinUnits)
    {
      errors.Add(Errors.SizeVariant.InvalidUnits);
    }

    if (units > 1 && singleSizeVariantId is null)
    {
      errors.Add(Errors.SizeVariant.MissingSingleSizeReference);
    }

    if (unitWeightInGrams is null && unitVolumeInMilliliters is null)
    {
      errors.Add(Errors.SizeVariant.MissingWeightOrVolume);
    }

    if (unitWeightInGrams is not null && unitVolumeInMilliliters is not null)
    {
      errors.Add(Errors.SizeVariant.WeightAndVolumeNotAllowed);
    }

    if (unitWeightInGrams < MinUnitWeightInGrams || unitVolumeInMilliliters < MinUnitVolumeInMilliliters)
    {
      errors.Add(Errors.SizeVariant.InvalidWeightOrVolume);
    }

    if (errors.Count > 0)
    {
      return errors;
    }

    return new SizeVariant(
        name,
      units,
      unitWeightInGrams ?? new(),
      unitVolumeInMilliliters ?? new(),
      singleSizeVariantId);
  }

  // TODO: invoked by relevant domain events, e.g. ProductLineSizeCreated, ProductLineSizeUpdated, ProductLineSizeDeleted
  public void AddProductLineSizeId(ProductLineSizeId productLineSizeId)
  {
    _productLineSizeIds.Add(productLineSizeId);
  }
}