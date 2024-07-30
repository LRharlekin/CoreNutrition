using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;

using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Infrastructure.Categories.Persistence;

public class CategoryConfigurations
  : IEntityTypeConfiguration<Category>
{
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    ConfigureCategoriesTable(builder);
  }

  private void ConfigureCategoriesTable(EntityTypeBuilder<Category> builder)
  {
    builder.ToTable("categories");

    builder.HasKey(c => c.Id);

    builder.Property(c => c.Id)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => CategoryId.Create(value)
      )
      .HasColumnName("id");

    builder.Property(c => c.Name)
      .IsRequired()
      .HasMaxLength(Category.Constraints.MaxNameLength)
      .HasColumnName("name");

    builder.HasIndex(c => c.Name)
      .IsUnique();

    builder.Property(c => c.Description)
      .IsRequired()
      .HasMaxLength(Category.Constraints.MaxDescriptionLength)
      .HasColumnName("description");

    builder.Property(c => c.CategoryImageUrl)
      .IsRequired()
      .HasConversion(
        value => value.OriginalString,
        value => new Uri(value)
      )
      .HasColumnName("category_image_url");

    builder.Property(c => c.CreatedDateTime)
      .IsRequired()
      .HasColumnName("created_date_time");

    builder.Property(c => c.UpdatedDateTime)
      .IsRequired()
      .HasColumnName("updated_date_time");

    // builder.OwnsMany(c => c.ProductLineIds);
    builder.OwnsMany(c => c.ProductLineIds, plBuilder =>
    {
      // plBuilder.ToTable("category_product_line_ids");

      // plBuilder.WithOwner().HasForeignKey("category_id");

      // plBuilder.Property<Guid>("id");

      // plBuilder.HasKey("id");

      // plBuilder.Property(plId => plId.Value)
      //   .HasColumnName("product_line_id")
      //   .IsRequired();
    });
  }
}
