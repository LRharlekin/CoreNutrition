using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;

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
    builder.ToTable(Names.Table);

    builder.HasKey(c => c.Id);

    builder.Property(c => c.Id)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => CategoryId.Create(value)
      )
      .HasColumnName(Names.IdColumn);

    builder.Property(c => c.Name)
      .IsRequired()
      .HasMaxLength(Category.Constraints.MaxNameLength)
      .HasColumnName(Names.NameColumn);

    builder.HasIndex(c => c.Name)
      .IsUnique();

    builder.Property(c => c.Description)
      .IsRequired()
      .HasMaxLength(Category.Constraints.MaxDescriptionLength)
      .HasColumnName(Names.DescriptionColumn);

    builder.Property(c => c.CategoryImageUrl)
      .IsRequired()
      .HasConversion(
        value => value.OriginalString,
        value => new Uri(value)
      )
      .HasColumnName(Names.CategoryImageUrlColumn);

    builder.Property(c => c.CreatedDateTime)
      .IsRequired()
      .HasColumnName(Names.CreatedDateTimeColumn);

    builder.Property(c => c.UpdatedDateTime)
      .IsRequired()
      .HasColumnName(Names.UpdatedDateTimeColumn);

    builder.Ignore(c => c.ProductLineIds);
  }

  private static class Names
  {
    public const string Table = "categories";
    public const string IdColumn = "id";
    public const string NameColumn = "name";
    public const string DescriptionColumn = "description";
    public const string CategoryImageUrlColumn = "category_image_url";
    public const string CreatedDateTimeColumn = "created_date_time";
    public const string UpdatedDateTimeColumn = "updated_date_time";
  }
}
