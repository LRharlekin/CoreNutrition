using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CoreNutrition.Domain.ProductLineAggregate;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;

using CoreNutrition.Domain.CategoryAggregate;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;

namespace CoreNutrition.Infrastructure.ProductLines.Persistence;

public class ProductLineConfigurations : IEntityTypeConfiguration<ProductLine>
{
  public void Configure(EntityTypeBuilder<ProductLine> builder)
  {
    ConfigureProductLinesTable(builder);
  }

  private void ConfigureProductLinesTable(EntityTypeBuilder<ProductLine> builder)
  {
    builder.ToTable(Names.Table);

    builder.HasKey(pl => pl.Id);

    builder.Property(pl => pl.Id)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => ProductLineId.Create(value))
      .HasColumnName(Names.IdColumn);

    builder.Property(pl => pl.Name)
      .HasMaxLength(ProductLine.Constraints.MaxNameLength)
      .HasColumnName(Names.NameColumn);

    builder.HasIndex(pl => pl.Name)
      .IsUnique();

    builder.Property(pl => pl.IsPublished)
      .IsRequired()
      .HasColumnName(Names.IsPublishedColumn);

    builder.HasOne<Category>()
      .WithMany()
      .HasForeignKey(pl => pl.CategoryId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(pl => pl.CategoryId)
      .HasConversion(
        id => id.Value,
        value => CategoryId.Create(value))
      .IsRequired()
      .HasColumnName(Names.CategoryIdColumn);

    builder.ComplexProperty(pl => pl.AverageRating, arBuilder =>
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

    builder.ComplexProperty(pl => pl.ProductLineInfo, pliBuilder =>
    {
      pliBuilder.Property(pli => pli.DescriptionShort)
        .HasMaxLength(ProductLineInfo.Constraints.MaxDescriptionShortLength)
        .IsRequired()
        .HasColumnName(Names.ProductLineInfo.DescriptionShortColumn);
      pliBuilder.Property(pli => pli.DescriptionLong)
        .HasMaxLength(ProductLineInfo.Constraints.MaxDescriptionLongLength)
        .IsRequired()
        .HasColumnName(Names.ProductLineInfo.DescriptionLongColumn);
      pliBuilder.Property(pli => pli.SuggestedUse)
        .HasMaxLength(ProductLineInfo.Constraints.MaxSuggestedUseLength)
        .IsRequired()
        .HasColumnName(Names.ProductLineInfo.SuggestedUseColumn);
      pliBuilder.Property(pli => pli.Benefit1)
        .HasMaxLength(ProductLineInfo.Constraints.MaxBenefitLength)
        .IsRequired()
        .HasColumnName(Names.ProductLineInfo.Benefit1Column);
      pliBuilder.Property(pli => pli.Benefit2)
        .HasMaxLength(ProductLineInfo.Constraints.MaxBenefitLength)
        .IsRequired()
        .HasColumnName(Names.ProductLineInfo.Benefit2Column);
      pliBuilder.Property(pli => pli.Benefit3)
        .HasMaxLength(ProductLineInfo.Constraints.MaxBenefitLength)
        .IsRequired()
        .HasColumnName(Names.ProductLineInfo.Benefit3Column);
      pliBuilder.Property(pli => pli.IsMuscleGain)
        .IsRequired()
        .HasColumnName(Names.ProductLineInfo.IsMuscleGainColumn);
      pliBuilder.Property(pli => pli.IsWeightLoss)
        .IsRequired()
        .HasColumnName(Names.ProductLineInfo.IsWeightLossColumn);
      pliBuilder.Property(pli => pli.IsHealthWellness)
        .IsRequired()
        .HasColumnName(Names.ProductLineInfo.IsHealthWellnessColumn);
    });

    builder.ComplexProperty(pl => pl.NutritionFacts, nfBuilder =>
    {
      nfBuilder.Property(nf => nf.CaloriesPer100Grams)
        .IsRequired()
        .HasColumnName(Names.NutritionFacts.CaloriesColumn);
      nfBuilder.Property(nf => nf.FatPer100Grams)
        .IsRequired()
        .HasColumnName(Names.NutritionFacts.FatsColumn);
      nfBuilder.Property(nf => nf.SaturatedFatPer100Grams)
        .IsRequired()
        .HasColumnName(Names.NutritionFacts.SaturatedFatsColumn);
      nfBuilder.Property(nf => nf.CarbohydratesPer100Grams)
        .IsRequired()
        .HasColumnName(Names.NutritionFacts.CarbohydratesColumn);
      nfBuilder.Property(nf => nf.SugarPer100Grams)
        .IsRequired()
        .HasColumnName(Names.NutritionFacts.SugarsColumn);
      nfBuilder.Property(nf => nf.ProteinPer100Grams)
        .IsRequired()
        .HasColumnName(Names.NutritionFacts.ProteinColumn);
      nfBuilder.Property(nf => nf.SaltPer100Grams)
        .IsRequired()
        .HasColumnName(Names.NutritionFacts.SaltColumn);
    });

    builder.Property(pls => pls.CreatedDateTime)
      .IsRequired()
      .HasColumnName(Names.CreatedDateTimeColumn);

    builder.Property(pls => pls.UpdatedDateTime)
      .IsRequired()
      .HasColumnName(Names.UpdatedDateTimeColumn);

    builder
      .Ignore(pl => pl.ProductIds)
      .Ignore(pl => pl.ProductLineSizeIds)
      .Ignore(pl => pl.ProductLineFlavourIds);
  }

  private static class Names
  {
    public const string Table = "product_lines";
    public const string IdColumn = "id";
    public const string NameColumn = "name";
    public const string CategoryIdColumn = "category_id";
    public const string IsPublishedColumn = "is_published";

    public static class AverageRating
    {
      public const string ScoreColumn = "average_rating_score";
      public const string NumRatingsColumn = "average_rating_num_ratings";
    }

    public static class ProductLineInfo
    {
      public const string DescriptionShortColumn = "description_short";
      public const string DescriptionLongColumn = "description_long";
      public const string SuggestedUseColumn = "suggested_use";
      public const string Benefit1Column = "benefit_1";
      public const string Benefit2Column = "benefit_2";
      public const string Benefit3Column = "benefit_3";
      public const string IsMuscleGainColumn = "is_muscle_gain";
      public const string IsWeightLossColumn = "is_weight_loss";
      public const string IsHealthWellnessColumn = "is_health_wellness";
    }

    public static class NutritionFacts
    {
      public const string CaloriesColumn = "calories_per_100_grams";
      public const string FatsColumn = "fat_per_100_grams";
      public const string SaturatedFatsColumn = "saturated_fat_per_100_grams";
      public const string CarbohydratesColumn = "carbohydrates_per_100_grams";
      public const string SugarsColumn = "sugar_per_100_grams";
      public const string ProteinColumn = "protein_per_100_grams";
      public const string SaltColumn = "salt_per_100_grams";
    }

    public const string CreatedDateTimeColumn = "created_date_time";
    public const string UpdatedDateTimeColumn = "updated_date_time";

    public static class ShadowProps
    {
      public const string AR_Score = "AverageRating_Score";
    }
  }
}