using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.Entities;


using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Infrastructure.ProductLineSizes.Persistence;

public class ProductLineSizeConfigurations
  : IEntityTypeConfiguration<ProductLineSize>
{
  public void Configure(EntityTypeBuilder<ProductLineSize> builder)
  {
    ConfigureProductLineSizesTable(builder);
    ConfigureSizeVariantsTable(builder);
  }

  private void ConfigureProductLineSizesTable(EntityTypeBuilder<ProductLineSize> builder)
  {
    builder.ToTable("product_line_sizes");

    // define the primary key
    builder.HasKey(pls => pls.Id);

    // configure primary key
    builder.Property(pls => pls.Id)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => ProductLineSizeId.Create(value)
      )
      .HasColumnName("id");

    // FK
    builder.Property(pls => pls.ProductLineId)
      .HasConversion(
        id => id.Value,
        value => ProductLineId.Create(value)
      )
      .IsRequired()
      .HasColumnName("product_line_id");

    // Extract SizeVariantId (FK) from SizeVariant
    // builder.HasOne(pls => pls.SizeVariant, sv =>
    // {
    //   sv.Property(sv => sv.Id)
    //     .HasConversion(
    //       id => id.Value,
    //       value => SizeVariantId.Create(value)
    //     )
    //     .IsRequired()
    //     .HasColumnName("size_variant_id");
    // }); // indicates navigation property on ProductLineSize

    // .WithOne()
    // .HasForeignKey<ProductLineSize>(pls => pls.SizeVariant.Id) // FK in ProductLineSize
    // .OnDelete(DeleteBehavior.Restrict); // prevent deletion of SizeVariant
    // .HasColumnName("SizeVariantId");

    // builder.Property(pls => pls.SizeVariant.Id)
    //   .HasConversion(
    //     id => id.Value,
    //     value => SizeVariantId.Create(value)
    //   )
    //   .IsRequired();

    // column name:RecommendedRetailPrice_Amount (not null)
    builder.OwnsOne(pls => pls.RecommendedRetailPrice, priceBuilder =>
    {
      priceBuilder.Property(rrp => rrp.Amount)
        .HasColumnName("recommended_retail_price_amount")
        .IsRequired();
      priceBuilder.Property(rrp => rrp.CurrencyCode)
        .HasColumnName("recommended_retail_price_currency_code")
        .IsRequired();
    });
  }

  private void ConfigureSizeVariantsTable(EntityTypeBuilder<ProductLineSize> builder)
  {
    builder.OwnsOne(pls => pls.SizeVariant, svBuilder =>
    {
      svBuilder.ToTable("size_variants");

      svBuilder.HasKey(sv => sv.Id);

      svBuilder.Property(sv => sv.Id)
        .ValueGeneratedNever()
        .HasConversion(
          id => id.Value,
          value => SizeVariantId.Create(value)
        )
        .HasColumnName("id");

      svBuilder.Property(sv => sv.Name)
        .HasMaxLength(SizeVariant.Constraints.MaxNameLength)
        .IsRequired()
        .HasColumnName("name");

      svBuilder.Property(sv => sv.Units)
        .IsRequired()
        .HasColumnName("units");

      svBuilder.Property(sv => sv.UnitWeightInGrams)
        .HasColumnName("unit_weight_in_grams");

      svBuilder.Property(sv => sv.UnitVolumeInMilliliters)
        .HasColumnName("unit_volume_in_milliliters");

      svBuilder.Property(sv => sv.SingleSizeVariantId)
        .HasConversion(
          id => id.Value,
          value => SizeVariantId.Create(value)
        )
        .HasColumnName("single_size_variant_id");
    });
  }
}