using Microsoft.EntityFrameworkCore;

using CoreNutrition.Domain.Common.Interfaces;
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
  private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
  public CoreNutritionDbContext
  (
    DbContextOptions<CoreNutritionDbContext> options,
    PublishDomainEventsInterceptor publishDomainEventsInterceptor)
    : base(options)
  {
    _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
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
    modelBuilder
      .Ignore<List<IDomainEvent>>()
      .ApplyConfiguration(new CategoryConfigurations())
      .ApplyConfiguration(new ProductLineSizeConfigurations())
      .ApplyConfiguration(new SizeVariantConfigurations())
      .ApplyConfiguration(new ProductConfigurations())
      .ApplyConfiguration(new ProductLineConfigurations())
      .ApplyConfiguration(new ProductLineFlavourConfigurations());

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

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);

    base.OnConfiguring(optionsBuilder);
  }
}