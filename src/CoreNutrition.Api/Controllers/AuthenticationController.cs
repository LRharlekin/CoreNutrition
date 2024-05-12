using Microsoft.AspNetCore.Mvc;

using CoreNutrition.Contracts.Authentication;
using CoreNutrition.Application.Services.Authentication;

namespace CoreNutrition.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
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

    var response = new AuthenticationResponse(
      authenticationResult.User.Id,
      authenticationResult.User.FirstName,
      authenticationResult.User.LastName,
      authenticationResult.User.Email,
      authenticationResult.Token);

    return Ok(response);
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