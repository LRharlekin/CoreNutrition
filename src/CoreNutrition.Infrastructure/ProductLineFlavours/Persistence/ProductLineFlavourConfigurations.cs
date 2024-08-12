using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;

using CoreNutrition.Domain.ProductLineAggregate;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

namespace CoreNutrition.Infrastructure.ProductLineFlavours.Persistence;

public class ProductLineFlavourConfigurations
  : IEntityTypeConfiguration<ProductLineFlavour>
  {
    public void Configure(EntityTypeBuilder<ProductLineFlavour> builder)
    {
      ConfigureProductLineFlavoursTable(builder);
    }

    private void ConfigureProductLineFlavoursTable(EntityTypeBuilder<ProductLineFlavour> builder)
    {
      builder.ToTable(Names.Table);

      builder.HasKey(plf => plf.Id);

      builder.Property(plf => plf.Id)
        .ValueGeneratedNever()
        .HasConversion(
          id => id.Value,
          value => ProductLineFlavourId.Create(value)
        )
        .HasColumnName(Names.IdColumn);

      builder.Property(plf => plf.Flavour)
        .IsRequired()
        .HasMaxLength(ProductLineFlavour.Constraints.MaxNameLength)
        .HasColumnName(Names.FlavourColumn);
      
      builder.HasIndex(plf => plf.Flavour)
        .IsUnique();

      builder.HasOne<ProductLine>()
        .WithMany()
        .HasForeignKey(plf => plf.ProductLineId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.Property(plf => plf.ProductLineId)
        .ValueGeneratedNever()
        .HasConversion(
          id => id.Value,
          value => ProductLineId.Create(value)
        )
        .IsRequired()
        .HasColumnName(Names.ProductLineIdColumn);

      builder.Property(plf => plf.FlavourImageUrl)
        .IsRequired()
        .HasConversion(
          value => value.OriginalString,
          value => new Uri(value)
        )
        .HasColumnName(Names.FlavourImageUrlColumn);

      builder.Property(plf => plf.CreatedDateTime)
        .IsRequired()
        .HasColumnName(Names.CreatedDateTimeColumn);

      builder.Property(plf => plf.UpdatedDateTime)
        .IsRequired()
        .HasColumnName(Names.UpdatedDateTimeColumn);

      builder.Ignore(plf => plf.ProductIds);
    }

    private static class Names
    {
      public const string Table = "product_line_flavours";
      public const string IdColumn = "id";
      public const string FlavourColumn = "flavour";
      public const string ProductLineIdColumn = "product_line_id";
      public const string FlavourImageUrlColumn = "flavour_image_url";

      public const string CreatedDateTimeColumn = "created_date_time";
      public const string UpdatedDateTimeColumn = "updated_date_time";
    }
  }