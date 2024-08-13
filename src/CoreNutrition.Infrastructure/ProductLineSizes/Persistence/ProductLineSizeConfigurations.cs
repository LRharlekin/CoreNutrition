using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;

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
  }

  private void ConfigureProductLineSizesTable(EntityTypeBuilder<ProductLineSize> builder)
  {
    builder.ToTable(Names.Table);

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
      .HasColumnName(Names.ProductLineIdColumn);

    // FK with navigation property ProductLineSize.SizeVariant
    builder.HasOne(pls => pls.SizeVariant)
      .WithMany()
      .HasForeignKey(Names.ShadowProps.SizeVariantId)
      .HasPrincipalKey(sv => sv.Id)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property<SizeVariantId>(Names.ShadowProps.SizeVariantId)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => SizeVariantId.Create(value)
      )
      .IsRequired()
      .HasColumnName(Names.SizeVariantIdColumn);

    builder.ComplexProperty(pls => pls.RecommendedRetailPrice, rrpBuilder =>
    {
      rrpBuilder.Property(rrp => rrp.Amount)
        .IsRequired()
        .HasColumnName(Names.RecommendedRetailPrice.AmountColumn);

      rrpBuilder.Property(rrp => rrp.CurrencyCode)
        .IsRequired()
        .HasMaxLength(CurrencyAmount.Constraints.CodeLength)
        .HasColumnName(Names.RecommendedRetailPrice.CurrencyCodeColumn);
    });

    // Define shadow property explicitly
    builder.Property<decimal>(Names.ShadowProps.RRP_Amount)
      .HasColumnName(Names.RecommendedRetailPrice.AmountColumn);

    // Use the shadow property to define the index
    builder.HasIndex(Names.ShadowProps.RRP_Amount)
      .HasDatabaseName(Names.RecommendedRetailPrice.AmountColumn);

    builder.Property(pls => pls.CreatedDateTime)
      .IsRequired()
      .HasColumnName(Names.CreatedDateTimeColumn);

    builder.Property(pls => pls.UpdatedDateTime)
      .IsRequired()
      .HasColumnName(Names.UpdatedDateTimeColumn);

    builder.Ignore(pls => pls.ProductIds);
  }

  private static class Names
  {
    public const string Table = "product_line_sizes";
    public const string IdColumn = "id";
    public const string ProductLineIdColumn = "product_line_id";
    public const string SizeVariantIdColumn = "size_variant_id";

    public static class RecommendedRetailPrice
    {
      public const string AmountColumn = "recommended_retail_price_amount";
      public const string CurrencyCodeColumn = "recommended_retail_price_currency_code";
    }

    public const string CreatedDateTimeColumn = "created_date_time";
    public const string UpdatedDateTimeColumn = "updated_date_time";

    public static class ShadowProps
    {
      public const string SizeVariantId = "SizeVariantId";
      public const string RRP_Amount = "RecommendedRetailPrice_Amount";
    }
  }
}