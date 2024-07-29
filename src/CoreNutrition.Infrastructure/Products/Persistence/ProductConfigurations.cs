using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CoreNutrition.Domain.ProductAggregate;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;

namespace CoreNutrition.Infrastructure.Products.Persistence;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    ConfigureProductsTable(builder);
    // FKs
    // ConfigureProductLinesTable(builder);
    // ConfigureProductLineSizesTable(builder);
    // ConfigureProductLineFlavoursTable(builder);

    // Referencing entities
    // reviewIds
    // orderLineItemIds
    // cartItemIds
  }

  private void ConfigureProductsTable(EntityTypeBuilder<Product> builder)
  {
    builder.ToTable("Products");

    builder.HasKey(p => p.Id);

    builder.Property(p => p.Id)
      .ValueGeneratedNever() // EF Core tries to generate ID in db by default
      .HasConversion(
        id => id.Value,
        value => ProductId.Create(value));

    builder.Property(p => p.Name)
      .HasMaxLength(Product.Constraints.MaxNameLength);

    

    builder.OwnsOne(p => p.AverageRating);

    builder.OwnsOne(p => p.RetailPrice);

    // FKs
    builder.Property(p => p.ProductLineId)
      .HasConversion(
        id => id.Value,
        value => ProductLineId.Create(value)
      );

    builder.Property(p => p.ProductLineSizeId)
      .HasConversion(
        id => id.Value,
        value => ProductLineSizeId.Create(value)
      );
      
    builder.Property(p => p.ProductLineFlavourId)
      .HasConversion(
        id => id.Value,
        value => ProductLineFlavourId.Create(value)
      );
  }
}