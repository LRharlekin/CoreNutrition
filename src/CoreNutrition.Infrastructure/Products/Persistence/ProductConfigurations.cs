using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CoreNutrition.Domain.ProductAggregate;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;

using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineAggregate;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineFlavourAggregate;

using CoreNutrition.Domain.Common.ValueObjects;

namespace CoreNutrition.Infrastructure.Products.Persistence;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    ConfigureProductsTable(builder);
  }

  private void ConfigureProductsTable(EntityTypeBuilder<Product> builder)
  {
    builder.ToTable(Names.Table);

    builder.HasKey(p => p.Id);

    builder.Property(p => p.Id)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => ProductId.Create(value)
      )
      .HasColumnName(Names.IdColumn);

    builder.Property(p => p.Name)
      .IsRequired()
      .HasMaxLength(Product.Constraints.MaxNameLength)
      .HasColumnName(Names.NameColumn);

    builder.Property(p => p.IsPublished)
      .IsRequired()
      .HasColumnName(Names.IsPublishedColumn);
    
// AverageRating
    builder.ComplexProperty(p => p.AverageRating, arBuilder =>
    {
      arBuilder.Property(ar => ar.Score)
        .HasColumnName(Names.AverageRating.ScoreColumn);

      arBuilder.Property(ar => ar.NumRatings)
        .IsRequired()
        .HasColumnName(Names.AverageRating.NumRatingsColumn);
    });

    builder.Property<double?>(Names.ShadowProps.AR_Score)
      .HasColumnName(Names.AverageRating.ScoreColumn);

    builder.HasIndex(Names.ShadowProps.AR_Score)
      .HasDatabaseName(Names.AverageRating.ScoreColumn);

//     RetailPrice
    builder.ComplexProperty(p => p.RetailPrice, rpBuilder =>
    {
      rpBuilder.Property(rp => rp.Amount)
        .IsRequired()
        .HasColumnName(Names.RetailPrice.AmountColumn);

      rpBuilder.Property(rp => rp.CurrencyCode)
        .IsRequired()
        .HasMaxLength(CurrencyAmount.Constraints.CodeLength)
        .HasColumnName(Names.RetailPrice.CurrencyCodeColumn);
    });

    builder.Property<decimal>(Names.ShadowProps.RP_Amount)
      .HasColumnName(Names.RetailPrice.AmountColumn);

    builder.HasIndex(Names.ShadowProps.RP_Amount)
      .HasDatabaseName(Names.RetailPrice.AmountColumn);

    builder.Property(p => p.QuantityInStock)
      .IsRequired()
      .HasColumnName(Names.QuantityInStockColumn);

// FKs without navigation property
    builder.HasOne<ProductLine>()
      .WithMany()
      .HasForeignKey(p => p.ProductLineId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(p => p.ProductLineId)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => ProductLineId.Create(value)
      )
      .IsRequired()
      .HasColumnName(Names.ProductLineIdColumn);

    builder.HasOne<ProductLineSize>()
      .WithMany()
      .HasForeignKey(p => p.ProductLineSizeId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(p => p.ProductLineSizeId)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => ProductLineSizeId.Create(value)
      )
      .IsRequired()
      .HasColumnName(Names.ProductLineSizeIdColumn);

    builder.HasOne<ProductLineFlavour>()
      .WithMany()
      .HasForeignKey(p => p.ProductLineFlavourId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(p => p.ProductLineFlavourId)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => ProductLineFlavourId.Create(value)
      )
      .IsRequired()
      .HasColumnName(Names.ProductLineFlavourIdColumn);

    builder.Property(p => p.IsVegan)
      .IsRequired()
      .HasColumnName(Names.IsVeganColumn);

    builder.Property(p => p.IsSample)
      .IsRequired()
      .HasColumnName(Names.IsSampleColumn);
    
    builder.Property(p => p.ProductImageUrl)
      .IsRequired()
      .HasConversion(
        value => value.OriginalString,
        value => new Uri(value)
      )
      .HasColumnName(Names.ProductImageUrlColumn);

    builder.Property(pls => pls.CreatedDateTime)
      .IsRequired()
      .HasColumnName(Names.CreatedDateTimeColumn);

    builder.Property(pls => pls.UpdatedDateTime)
      .IsRequired()
      .HasColumnName(Names.UpdatedDateTimeColumn);

    builder
      .Ignore(p => p.ReviewIds)
      .Ignore(p => p.OrderLineItemIds)
      .Ignore(p => p.CartItemIds);
  }

  private static class Names
  {
    public const string IdColumn = "id";
    public const string Table = "products";
    public const string NameColumn = "name";
    public const string IsPublishedColumn = "is_published";

    public static class AverageRating
    {
      public const string ScoreColumn = "average_rating_score";
      public const string NumRatingsColumn = "average_rating_num_ratings";
    }

    public static class RetailPrice
    {
      public const string AmountColumn = "retail_price_amount";
      public const string CurrencyCodeColumn = "retail_price_currency_code";
    }

    public const string QuantityInStockColumn = "quantity_in_stock";
    public const string ProductLineIdColumn = "product_line_id";
    public const string ProductLineSizeIdColumn = "product_line_size_id";
    public const string ProductLineFlavourIdColumn = "product_line_flavour_id";

    public const string IsVeganColumn = "is_vegan";
    public const string IsSampleColumn = "is_sample";
    public const string ProductImageUrlColumn = "product_image_url";

    public const string CreatedDateTimeColumn = "created_date_time";
    public const string UpdatedDateTimeColumn = "updated_date_time";

    public static class ShadowProps
    {
      public const string RP_Amount = "RetailPrice_Amount";
      public const string AR_Score = "AverageRating_Score";
    }
  }
}