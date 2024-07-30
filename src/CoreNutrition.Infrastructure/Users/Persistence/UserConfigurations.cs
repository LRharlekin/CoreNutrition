// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// using CoreNutrition.Domain.UserAggregate;
// using CoreNutrition.Domain.UserAggregate.ValueObjects;

// namespace CoreNutrition.Infrastructure.Users.Persistence;

// // TODO: Delete later: ?v=IlXnIe6p_Uk 2:35

// public class UserConfigurations : IEntityTypeConfiguration<User>
// {
//     public void Configure(EntityTypeBuilder<User> builder)
//     {
//         builder.ToTable("Users");

//         builder.HasKey(u => u.Id);

//         builder.Property(u => u.Id)
//             .ValueGeneratedNever()
//             .HasConversion(
//                 id => id.Value,
//                 value => UserId.Create(value));

//         // builder.Property(u => u.Email)
//         //     .HasConversion(
//         //         email => email.Value,
//         //         value => Email.CreateNew(value)
//         //     )
//         //     .HasMaxLength(Email.Constraints.MaxLength);

//         // builder.Property(u => u.PasswordHash)
//         //     .HasMaxLength(128);

//         // builder.Property(u => u.FirstName)
//         //     .HasConversion(
//         //         firstName => firstName.Value,
//         //         value => FirstName.CreateNew(value)
//         //     )
//         //     .HasMaxLength(FirstName.Constraints.MaxLength);

//         // builder.Property(u => u.LastName)
//         //     .HasConversion(
//         //         lastName => lastName.Value,
//         //         value => LastName.CreateNew(value)
//         //     )
//         //     .HasMaxLength(LastName.Constraints.MaxLength);

//         // builder.HasIndex(u => u.Email)
//         //     .IsUnique();
//     }
// }

