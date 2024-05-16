public interface PasswordHasher
{
  string HashPassword(string password);
  bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}