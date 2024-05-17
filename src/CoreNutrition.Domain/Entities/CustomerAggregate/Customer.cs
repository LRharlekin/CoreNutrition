using CoreNutrition.Domain.Common.Models;

namespace CoreNutrition.Domain.CustomerAggregate;

public sealed class Customer : AggregateRoot<CustomerId, Guid>
{
  public string Email { get; private set; }
  public string PasswordHash { get; private set; }
  public string? FirstName { get; private set; }
  public string? LastName { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public DateTime UpdatedAt { get; private set; }

  private Customer(string email, string passwordHash, string? firstName, string? lastName, CustomerId? customerId = null) : base(customerId ?? CustomerId.CreateUnique())
  {
    Email = email;
    PasswordHash = passwordHash;
    FirstName = firstName;
    LastName = lastName;
    CreatedAt = DateTime.UtcNow;
    UpdatedAt = DateTime.UtcNow;
  }

  public static Customer Create(string email, string passwordHash, string firstName, string lastName)
  {
    return new Customer(
      email,
      passwordHash,
      firstName,
      lastName);
  }

#pragma warning disable CS8618
  private Customer()
  {
    // Required by EF
  }
#pragma warning disable CS8618
}