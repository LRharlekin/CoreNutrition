using CoreNutrition.Domain.UserAggregate.ValueObjects;

namespace CoreNutrition.Application.Common.Interfaces.Cryptography;
public interface IPasswordHasher
{
  string HashPassword(Password password);
}