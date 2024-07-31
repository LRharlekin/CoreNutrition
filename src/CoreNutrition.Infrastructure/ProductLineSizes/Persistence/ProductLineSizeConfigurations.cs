using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.Entities;


using CoreNutrition.Domain.ProductLineAggregate;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.Common.ValueObjects;

namespace CoreNutrition.Infrastructure.ProductLineSizes.Persistence;

public class ProductLineSizeConfigurations
  : IEntityTypeConfiguration<ProductLineSize>
{

  public void Configure(EntityTypeBuilder<ProductLineSize> builder)
  {
    ConfigureProductLineSizesTable(builder);
    // ConfigureSizeVariantsTable(builder);
  }

  private void ConfigureProductLineSizesTable(EntityTypeBuilder<ProductLineSize> builder)
  {
    builder.ToTable(Names.PLS.Table);

    builder.HasKey(pls => pls.Id);

    builder.Property(pls => pls.Id)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => ProductLineSizeId.Create(value)
      )
      .HasColumnName(Names.IdColumn);

    // FK without navigation property
    builder.HasOne<ProductLine>()
      .WithMany()
      .HasForeignKey(pls => pls.ProductLineId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(pls => pls.ProductLineId)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => ProductLineId.Create(value)
      )
      .IsRequired()
      .HasColumnName(Names.PLS.ProductLineIdColumn);

    builder.HasIndex(pls => pls.ProductLineId);

    // FK with navigation property ProductLineSize.SizeVariant
    builder.HasOne(pls => pls.SizeVariant)
      .WithMany()
      .HasForeignKey(Names.ShadowProps.SizeVariantId)
      .HasPrincipalKey(sv => sv.Id)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property<SizeVariantId>(Names.ShadowProps.SizeVariantId)
    // builder.Property(pls => pls.SizeVariant.Id)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => SizeVariantId.Create(value)
      )
      .IsRequired()
      .HasColumnName(Names.PLS.SizeVariantIdColumn);

    builder.HasIndex(Names.ShadowProps.SizeVariantId);
    // builder.HasIndex(pls => pls.SizeVariant.Id);

    builder.ComplexProperty(pls => pls.RecommendedRetailPrice, rrpBuilder =>
    {
      rrpBuilder.Property(rrp => rrp.Amount)
        .IsRequired()
        .HasColumnName(Names.PLS.RecommendedRetailPriceAmountColumn);
      rrpBuilder.Property(rrp => rrp.CurrencyCode)
        .IsRequired()
        .HasMaxLength(CurrencyAmount.Constraints.CodeLength)
        .HasColumnName(Names.PLS.RecommendedRetailPriceCurrencyCodeColumn);
    });


    // builder.HasIndex(pls => new { pls.RecommendedRetailPrice.Amount });

    builder.Property(pls => pls.CreatedDateTime)
      .IsRequired()
      .HasColumnName(Names.CreatedDateTimeColumn);

    builder.Property(pls => pls.UpdatedDateTime)
      .IsRequired()
      .HasColumnName(Names.UpdatedDateTimeColumn);

    builder.Ignore(pls => pls.ProductIds);
  }

  // private void ConfigureSizeVariantsTable(EntityTypeBuilder<ProductLineSize> builder)
  // {
  //   builder.OwnsOne(pls => pls.SizeVariant, svBuilder =>
  //   {
  //     svBuilder.ToTable(Names.SV.Table);

  //     svBuilder.HasKey(sv => sv.Id);

  //     svBuilder.Property(sv => sv.Id)
  //       .ValueGeneratedNever()
  //       .HasConversion(
  //         id => id.Value,
  //         value => SizeVariantId.Create(value)
  //       )
  //       .HasColumnName(Names.IdColumn);

  //     svBuilder.Property(sv => sv.Name)
  //       .HasMaxLength(SizeVariant.Constraints.MaxNameLength)
  //       .IsRequired()
  //       .HasColumnName(Names.SV.NameColumn);

  //     svBuilder.Property(sv => sv.Units)
  //       .IsRequired()
  //       .HasColumnName(Names.SV.UnitsColumn);

  //     svBuilder.Property(sv => sv.UnitWeightInGrams)
  //       .HasColumnName(Names.SV.UnitWeightInGramsColumn);

  //     svBuilder.Property(sv => sv.UnitVolumeInMilliliters)
  //       .HasColumnName(Names.SV.UnitVolumeInMillilitersColumn);

  //     // FK without navigation property present
  //     svBuilder.HasOne<SizeVariant>()
  //       .WithMany()
  //       .HasForeignKey(sv => sv.SingleSizeVariantId)
  //       .OnDelete(DeleteBehavior.Restrict);

  //     svBuilder.Property(sv => sv.SingleSizeVariantId)
  //       .ValueGeneratedNever()
  //       .HasConversion(
  //         id => (Guid?)id.Value,
  //         value => value.HasValue ? SizeVariantId.Create(value.Value) : null
  //         // value => value == null ? (SizeVariantId?)null : SizeVariantId.Create(value)
  //         // value => SizeVariantId.Create(value)
  //       )
  //       .IsRequired(false)
  //       .HasColumnName(Names.SV.SingleSizeVariantIdColumn);

  //     svBuilder.Ignore(sv => sv.ProductLineSizeIds);
  //   });
  // }

  private static class Names
  {
    public const string IdColumn = "id";
    public const string CreatedDateTimeColumn = "created_date_time";
    public const string UpdatedDateTimeColumn = "updated_date_time";

    public static class ShadowProps
    {
      public const string SizeVariantId = "SizeVariantId";
    }

    public static class PLS
    {
      public const string Table = "product_line_sizes";
      public const string ProductLineIdColumn = "product_line_id";
      public const string SizeVariantIdColumn = "size_variant_id";
      public const string RecommendedRetailPriceAmountColumn = "recommended_retail_price_amount";
      public const string RecommendedRetailPriceCurrencyCodeColumn = "recommended_retail_price_currency_code";
    }

    public static class SV
    {
      public const string Table = "size_variants";
      public const string NameColumn = "name";
      public const string UnitsColumn = "units";
      public const string UnitWeightInGramsColumn = "unit_weight_in_grams";
      public const string UnitVolumeInMillilitersColumn = "unit_volume_in_milliliters";
      public const string SingleSizeVariantIdColumn = "single_size_variant_id";
    }
  }
}