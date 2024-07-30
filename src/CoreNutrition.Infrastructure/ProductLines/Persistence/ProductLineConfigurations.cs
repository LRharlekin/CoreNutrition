// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// using CoreNutrition.Domain.ProductLineAggregate;
// using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
// using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
// using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;

// using CoreNutrition.Domain.CategoryAggregate;
// using CoreNutrition.Domain.CategoryAggregate.ValueObjects;

// namespace CoreNutrition.Infrastructure.ProductLines.Persistence;

// public class ProductLineConfigurations : IEntityTypeConfiguration<ProductLine>
// {
//   public void Configure(EntityTypeBuilder<ProductLine> builder)
//   {
//     ConfigureProductLinesTable(builder);
//     // FKs
//     // ConfigureCategoriesTable(builder);

//     // Referencing entities
//     // products
//     // productLineSizes
//     // productLineFlavours
//   }

//   private void ConfigureProductLinesTable(EntityTypeBuilder<ProductLine> builder)
//   {
//     builder.ToTable("ProductLines");

//     builder.HasKey(p => p.Id);

//     builder.Property(p => p.Id)
//       .ValueGeneratedNever() // EF Core tries to generate ID in db by default
//       .HasConversion(
//         id => id.Value,
//         value => ProductLineId.Create(value));

//     builder.Property(p => p.Name)
//       .HasMaxLength(ProductLine.Constraints.MaxNameLength);

//     // contained entity (FK)
//     // use generic if there is no navigation property on Category
//     // builder.HasOne<Category>()
//     //   .WithMany()
//     //   .HasForeignKey(p => p.CategoryId)
//     //   .IsRequired();

//     // FK: category
//     builder.Property(p => p.CategoryId)
//       .HasConversion(
//         id => id.Value,
//         value => CategoryId.Create(value)
//       );

//       // isPublished

// // VO: average rating
//     builder.OwnsOne(p => p.AverageRating, arBuilder => {
//       arBuilder.Property(ar => ar.Score);
//       arBuilder.Property(ar => ar.NumRatings).IsRequired(true);
//     });


//   // VO: product line info

//   // VO: nutrition facts


//   //   builder.Property(p => p.ProductLineSizeId)
//   //     .HasConversion(
//   //       id => id.Value,
//   //       value => ProductLineSizeId.Create(value)
//   //     );

//   //   builder.Property(p => p.ProductLineFlavourId)
//   //     .HasConversion(
//   //       id => id.Value,
//   //       value => ProductLineFlavourId.Create(value)
//   //     );
//   }
// }