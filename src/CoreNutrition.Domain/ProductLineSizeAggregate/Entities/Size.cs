using CoreNutrition.Domain.Common.Models;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;

namespace CoreNutrition.Domain.ProductLineSizeAggregate.Entities;

public sealed class Size : Entity<SizeId>
{
  private List<ProductLineSizeId> _productLineSizeIds = new();

  public string Name { get; private set; }
  public int UnitWeightInGrams { get; private set; }
  public int UnitVolumeInMilliliters { get; private set; }
  public int Units { get; private set; }
  public SizeId? SingleSizeId { get; private set; }
  public IReadOnlyList<ProductLineSizeId> ProductLineSizeIds => _productLineSizeIds.AsReadOnly();  // related / referenced entities


  private Size(
    string name,
    int unitWeightInGrams,
    int unitVolumeInMilliliters,
    int units,
    SizeId singleSizeId)
    : base(SizeId.CreateUnique())
  {
    Name = name;
    UnitWeightInGrams = unitWeightInGrams;
    UnitVolumeInMilliliters = unitVolumeInMilliliters;
    Units = units;
    SingleSizeId = singleSizeId;
  }

  public static Size Create(
    string name,
    int unitWeightInGrams,
    int unitVolumeInMilliliters,
    int units,
    SizeId singleSizeId)
  {
    return new Size(
      name,
      unitWeightInGrams,
      unitVolumeInMilliliters,
      units,
      singleSizeId);
  }

  // TODO: invoked by relevant domain events, e.g. ProductLineSizeCreated, ProductLineSizeUpdated, ProductLineSizeDeleted
  public void AddProductLineSizeId(ProductLineSizeId productLineSizeId)
  {
    _productLineSizeIds.Add(productLineSizeId);
  }
}