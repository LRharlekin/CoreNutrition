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
    Console.WriteLine($"First name: {request.FirstName}");
    Console.WriteLine($"Last name: {request.LastName}");
    Console.WriteLine($"Email: {request.Email}");
    Console.WriteLine($"Password: {request.Password}");


    var authResult = _authenticationService.Register(
      request.FirstName,
      request.LastName,
      request.Email,
      request.Password);

    var response = new AuthenticationResponse(
      authResult.Id,
      authResult.FirstName,
      authResult.LastName,
      authResult.Email,
      authResult.Token);

    return Ok(response);
  }

  [HttpPost("login")]
  public IActionResult Login(LoginRequest request)
  {
    var authResult = _authenticationService.Login(
      request.Email,
      request.Password);

    var response = new AuthenticationResponse(
      authResult.Id,
      authResult.FirstName,
      authResult.LastName,
      authResult.Email,
      authResult.Token);
    return Ok(response);
  }
}