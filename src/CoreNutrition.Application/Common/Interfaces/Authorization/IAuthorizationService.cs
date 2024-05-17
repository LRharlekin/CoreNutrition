using CoreNutrition.Application.Common.Security;

using ErrorOr;

namespace CoreNutrition.Application.Common.Interfaces.Authorization;

public interface IAuthorizationService
{
  ErrorOr<Success> AuthorizeCurrentUser<T>(
      IAuthorizeableRequest<T> request,
      // List<string> requiredPermissions,
      List<string> requiredRoles);
}