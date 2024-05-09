using CoreNutrition.Domain.Common.Models;

namespace CoreNutrition.Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId, Guid>
{
  public string Email { get; private set;}
  public string PasswordHash { get; private set; }
  public string? FirstName { get; private set; }
  public string? LastName { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public DateTime UpdatedAt { get; private set; }

  private User(string email, string passwordHash, string? firstName, string? lastName, UserId? userId = null) : base(userId ?? UserId.CreateUnique())
  {
    Email = email;
    PasswordHash = passwordHash;
    FirstName = firstName;
    LastName = lastName;
    CreatedAt = DateTime.UtcNow;
    UpdatedAt = DateTime.UtcNow;
  }

  public static User Create(string email, string passwordHash, string firstName, string lastName)
  {
    return new User(
      email, 
      passwordHash, 
      firstName, 
      lastName);
  }

  #pragma warning disable CS8618
  private User()
  {
    // Required by EF
  }
  #pragma warning disable CS8618
}