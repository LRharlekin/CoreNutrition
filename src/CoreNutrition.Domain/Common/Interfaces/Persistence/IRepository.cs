namespace CoreNutrition.Domain.Common.Interfaces.Persistence;

public interface IRepository
{
  public void Add(string key, string value);
  public void Update(string key, string value);
  public void Remove(string key);
}