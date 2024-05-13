using Microsoft.AspNetCore.Mvc;

using CoreNutrition.Contracts.Authentication;
using CoreNutrition.Application.Services.Authentication;

namespace CoreNutrition.Api.Controllers;

// Typical Controller Logic: Map > Logic > Map
// map request to internal service models, 
// logic (e.g. persist in db), 
// map returned object to response contract

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
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
    AuthenticationResult authenticationResult = _authenticationService.Register(
      request.FirstName,
      request.LastName,
      request.Email,
      request.Password);

    // TODO: Save user to database

    var response = new AuthenticationResponse(
      authenticationResult.User.Id,
      authenticationResult.User.FirstName,
      authenticationResult.User.LastName,
      authenticationResult.User.Email,
      authenticationResult.Token);

    // return Ok(response);

    /* 
    ControllerBase.CreatedAtAction(
      String actionNamme, // The name of the action to use for generating the URL --> nameof(Register)
      Object routeValues, // The route data to use for generating the URL in the response's Location header --> new { id = user.Id}
      Object value); // The content value to format in the entity body --> response
    */

    return CreatedAtAction(
      actionName: nameof(Register),
      routeValues: new { id = authenticationResult.User.Id },
      value: response
    );
  }

  [HttpPost("login")]
  public IActionResult Login(LoginRequest request)
  {
    var authResult = _authenticationService.Login(
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
}