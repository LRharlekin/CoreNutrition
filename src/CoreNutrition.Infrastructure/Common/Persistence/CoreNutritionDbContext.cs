using Microsoft.EntityFrameworkCore;

using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.ProductAggregate;
using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.ProductLineSizeAggregate.Entities;
using CoreNutrition.Domain.ProductLineAggregate;
using CoreNutrition.Domain.ProductLineFlavourAggregate;

using CoreNutrition.Infrastructure.Categories.Persistence;
using CoreNutrition.Infrastructure.Products.Persistence;
using CoreNutrition.Infrastructure.ProductLineSizes.Persistence;
using CoreNutrition.Infrastructure.ProductLines.Persistence;
using CoreNutrition.Infrastructure.ProductLineFlavours.Persistence;

namespace CoreNutrition.Infrastructure.Common.Persistence;

public class CoreNutritionDbContext : DbContext
{
  public CoreNutritionDbContext
  (DbContextOptions<CoreNutritionDbContext> options)
    : base(options)
  {
    // TODO: Add DbContext
  }

  public DbSet<Category> Categories { get; set; } = null!;
  public DbSet<ProductLineSize> ProductLineSizes { get; set; } = null!;
  public DbSet<SizeVariant> SizeVariants { get; set; } = null!;
  public DbSet<ProductLine> ProductLines { get; set; } = null!;
  public DbSet<ProductLineFlavour> ProductLineFlavours { get; set; } = null!;
  public DbSet<Product> Products { get; set; } = null!;
  // public DbSet<Product> Products { get; set; } = null!;
  // public DbSet<Product> Products { get; set; } = null!;
  // public DbSet<Product> Products { get; set; } = null!;
  // public DbSet<Product> Products { get; set; } = null!;
  // public DbSet<Product> Products { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // modelBuilder
    // .ApplyConfigurationsFromAssembly(typeof(CoreNutritionDbContext).Assembly);
    modelBuilder.ApplyConfiguration(new CategoryConfigurations());
    modelBuilder.ApplyConfiguration(new ProductLineSizeConfigurations());
    modelBuilder.ApplyConfiguration(new SizeVariantConfigurations());
    modelBuilder.ApplyConfiguration(new ProductConfigurations());
    modelBuilder.ApplyConfiguration(new ProductLineConfigurations());
    modelBuilder.ApplyConfiguration(new ProductLineFlavourConfigurations());

    modelBuilder.Model
      .GetEntityTypes()
      .SelectMany(e => e.GetProperties())
      .Where(p => p.IsPrimaryKey())
      .ToList()
      .ForEach(p =>
      {
        p.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never;
      });

    base.OnModelCreating(modelBuilder);
  }
}