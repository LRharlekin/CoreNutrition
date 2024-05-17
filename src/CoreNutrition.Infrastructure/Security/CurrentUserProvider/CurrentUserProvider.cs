using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using ErrorOr;

namespace CoreNutrition.Infrastructure.Security.CurrentUserProvider;

public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
{
  public ErrorOr<CurrentUser> GetCurrentUser()
  {
    if (_httpContextAccessor.HttpContext is null)
    {
      return Error.Failure("Failure: HttpContext is null. Cannot read current user from HttpContext.");
    }

    var id = Guid.Parse(JwtRegisteredClaimNames.Sub);
    var firstName = GetSingleClaimValue(JwtRegisteredClaimNames.GivenName);
    var lastName = GetSingleClaimValue(JwtRegisteredClaimNames.FamilyName);
    var email = GetSingleClaimValue(JwtRegisteredClaimNames.Email);
    // var permissions = GetClaimValues("permissions");
    var roles = GetClaimValues(ClaimTypes.Role);

    return new CurrentUser(
      id,
      firstName,
      lastName,
      email,
      // permissions,
      roles);
  }

  private List<string> GetClaimValues(string claimType) =>
      _httpContextAccessor.HttpContext!.User.Claims
          .Where(claim => claim.Type == claimType)
          .Select(claim => claim.Value)
          .ToList();

  private string GetSingleClaimValue(string claimType) =>
      _httpContextAccessor.HttpContext!.User.Claims
          .Single(claim => claim.Type == claimType)
          .Value;
}