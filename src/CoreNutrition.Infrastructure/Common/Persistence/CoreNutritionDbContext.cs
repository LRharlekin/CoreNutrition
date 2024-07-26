using CoreNutrition.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace CoreNutrition.Infrastructure.Common.Persistence;

public class CoreNutritionDbContext : DbContext
{
  public CoreNutritionDbContext
  (DbContextOptions<CoreNutritionDbContext> options)
    : base(options)
  {
    // TODO: Add DbContext
  }

  public DbSet<Product> Products { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder
      .ApplyConfigurationsFromAssembly(typeof(CoreNutritionDbContext).Assembly);

    base.OnModelCreating(modelBuilder);
  }
}