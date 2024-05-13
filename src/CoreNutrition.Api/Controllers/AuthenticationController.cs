using Microsoft.AspNetCore.Mvc;

using ErrorOr;

using CoreNutrition.Contracts.Authentication;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Application.Services.Authentication.Common;
using CoreNutrition.Application.Services.Authentication.Commands;
using CoreNutrition.Application.Services.Authentication.Queries;

namespace CoreNutrition.Api.Controllers;

// Typical Controller Logic: Map > Logic > Map
// map request to internal service models, 
// logic (e.g. persist in db), 
// map returned object to response contract

[ApiController]
[Route("auth")]
public class AuthenticationController : ApiControllerBase
// ControllerBase docs: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-8.0
{
  private readonly IAuthenticationCommandService _authenticationCommandService;
  private readonly IAuthenticationQueryService _authenticationQueryService;

  public AuthenticationController(
    IAuthenticationCommandService authenticationCommandService,
    IAuthenticationQueryService authenticationQueryService)
  {
    _authenticationCommandService = authenticationCommandService;
    _authenticationQueryService = authenticationQueryService;
  }

  [HttpPost("register")]
  public IActionResult Register(RegisterRequest request)
  {
    ErrorOr<AuthenticationResult> authResult = _authenticationCommandService.Register(
      request.FirstName,
      request.LastName,
      request.Email,
      request.Password);

    // TODO: Save user to database

    return authResult.Match(
    // return authResult.MatchFirst(
      authResult => Ok(MapAuthResult(authResult)),
      errors => Problem(errors)
      // firstError => Problem(
      //   statusCode: StatusCodes.Status409Conflict,
      //   title: firstError.Description
      // )
      );
  }

  [HttpPost("login")]
  public IActionResult Login(LoginRequest request)
  {
    // multiple expected custom errors: 
    ErrorOr<AuthenticationResult> authResult = _authenticationQueryService.Login(
      request.Email,
      request.Password);

    // handle invalid credentials
    if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
    {
      return Problem(
        statusCode: StatusCodes.Status401Unauthorized,
        title: authResult.FirstError.Description);
    }

    return authResult.Match(
      authResult => Ok(MapAuthResult(authResult)),
      errors => Problem(errors)
      );
  }

  private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
  {
    return new AuthenticationResponse(
      authResult.User.Id,
      authResult.User.FirstName,
      authResult.User.LastName,
      authResult.User.Email,
      authResult.Token);
  }
}