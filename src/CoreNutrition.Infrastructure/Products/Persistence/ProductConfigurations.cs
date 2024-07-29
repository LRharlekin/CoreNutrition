using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CoreNutrition.Domain.ProductAggregate;

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
  }
}