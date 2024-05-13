using Microsoft.AspNetCore.Mvc;

using ErrorOr;

using CoreNutrition.Contracts.Authentication;
using CoreNutrition.Application.Services.Authentication;

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
  private readonly IAuthenticationService _authenticationService;

  public AuthenticationController(IAuthenticationService authenticationService)
  {
    _authenticationService = authenticationService;
  }

  [HttpPost("register")]
  public IActionResult Register(RegisterRequest request)
  {
    ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(
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
    ErrorOr<AuthenticationResult> authResult = _authenticationService.Login(
      request.Email,
      request.Password);

    var response = new AuthenticationResponse(
      authResult.User.Id,
      authResult.User.FirstName,
      authResult.User.LastName,
      authResult.User.Email,
      authResult.Token);
    return Ok(response);
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