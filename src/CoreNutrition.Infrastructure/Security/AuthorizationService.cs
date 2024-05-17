using CoreNutrition.Application.Common.Interfaces.Authorization;
using CoreNutrition.Application.Common.Security;
using CoreNutrition.Infrastructure.Security.CurrentUserProvider;

using ErrorOr;

namespace CleanArchitecture.Infrastructure.Security;

public class AuthorizationService(
    ICurrentUserProvider _currentUserProvider)
        : IAuthorizationService
{
  public ErrorOr<Success> AuthorizeCurrentUser<T>(
      IAuthorizeableRequest<T> request,
      // List<string> requiredPermissions,
      List<string> requiredRoles)
  {
    var currentUser = _currentUserProvider.GetCurrentUser();

    // if (requiredPermissions.Except(currentUser.Permissions).Any())
    // {
    //   return Error.Unauthorized(description: "User is missing required permissions for taking this action");
    // }

    if (requiredRoles.Except(currentUser.Roles).Any())
    {
      return Error.Unauthorized(description: "User is missing required roles for taking this action");
    }

    return Result.Success;
  }
}