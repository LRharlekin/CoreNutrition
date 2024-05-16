namespace CoreNutrition.Domain.UserAggregate;

public class User
{
  public Guid Id { get; } = Guid.NewGuid();
  public string FirstName { get; } = null!;
  public string LastName { get; } = null!;
  public string Email { get; } = null!;
  public string Password { get; } = null!;
  // public byte[] PasswordHash { get; set; } = null!;
  // public byte[] PasswordSalt { get; set; } = null!;

  public User(
    string firstName,
    string lastName,
    string email,
    string password
  )
  {
    // enforce invariants
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    Password = password;
  }

}