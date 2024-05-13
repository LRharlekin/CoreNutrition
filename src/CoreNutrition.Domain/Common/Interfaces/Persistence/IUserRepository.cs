using CoreNutrition.Domain.UserAggregate;

namespace CoreNutrition.Domain.Common.Interfaces.Persistence;

public interface IUserRepository
{
  User? GetUserByEmail(string email);
  public void Add(User user);
}