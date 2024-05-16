namespace CoreNutrition.Contracts.Authentication;

public record RegisterRequest(
  string FirstName,
  string LastName,
  string Email,
  string Password
// string PasswordHash,
// string PasswordSalt
);