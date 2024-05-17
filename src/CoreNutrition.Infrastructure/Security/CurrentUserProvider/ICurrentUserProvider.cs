using ErrorOr;

namespace CoreNutrition.Infrastructure.Security.CurrentUserProvider;

public interface ICurrentUserProvider
{
  CurrentUser GetCurrentUser();
}