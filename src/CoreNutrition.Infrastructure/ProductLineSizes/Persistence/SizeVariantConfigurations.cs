using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.Entities;

namespace CoreNutrition.Infrastructure.ProductLineSizes.Persistence;

public class SizeVariantConfigurations
  : IEntityTypeConfiguration<SizeVariant>
{

  public void Configure(EntityTypeBuilder<SizeVariant> builder)
  {
    ConfigureSizeVariantsTable(builder);
  }

  private void ConfigureSizeVariantsTable(EntityTypeBuilder<SizeVariant> builder)
  {

    builder.ToTable(Names.Table);

    builder.HasKey(sv => sv.Id);

    builder.Property(sv => sv.Id)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => SizeVariantId.Create(value)
      )
      .HasColumnName(Names.IdColumn);

    builder.Property(sv => sv.Name)
      .HasMaxLength(SizeVariant.Constraints.MaxNameLength)
      .IsRequired()
      .HasColumnName(Names.NameColumn);

    builder.Property(sv => sv.Units)
      .IsRequired()
      .HasColumnName(Names.UnitsColumn);

    builder.Property(sv => sv.UnitWeightInGrams)
      .HasColumnName(Names.UnitWeightInGramsColumn);

    builder.Property(sv => sv.UnitVolumeInMilliliters)
      .HasColumnName(Names.UnitVolumeInMillilitersColumn);

    // FK without navigation property present
    builder.HasOne<SizeVariant>()
      .WithMany()
      .HasForeignKey(sv => sv.SingleSizeVariantId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(sv => sv.SingleSizeVariantId)
      .ValueGeneratedNever()
      .HasConversion(
        id => id != null ? id.Value : default(Guid?),
        value => value.HasValue ? SizeVariantId.Create(value.Value) : default
      )
      .IsRequired(false)
      .HasColumnName(Names.SingleSizeVariantIdColumn);

    builder.Ignore(sv => sv.ProductLineSizeIds);
  }


  private static class Names
  {
    public const string Table = "size_variants";
    public const string IdColumn = "id";
    public const string NameColumn = "name";
    public const string UnitsColumn = "units";
    public const string UnitWeightInGramsColumn = "unit_weight_in_grams";
    public const string UnitVolumeInMillilitersColumn = "unit_volume_in_milliliters";
    public const string SingleSizeVariantIdColumn = "single_size_variant_id";

  }
}