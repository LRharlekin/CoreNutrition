using CoreNutrition.Application.Common.Security.Request;

using ErrorOr;

namespace CoreNutrition.Application.Common.Security;

public interface IAuthorizationService
{
  ErrorOr<Success> AuthorizeCurrentUser<T>(
      IAuthorizeableRequest<T> request,
      List<string> requiredRoles,
      List<string> requiredPermissions,
      List<string> requiredPolicies);
}