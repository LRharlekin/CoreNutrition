using Microsoft.AspNetCore.Mvc;

using ErrorOr;
using MediatR;

using CoreNutrition.Contracts.Authentication;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Application.Authentication.Common;
using CoreNutrition.Application.Authentication.Commands.Register;
using CoreNutrition.Application.Authentication.Queries.Login;

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
  private readonly ISender _mediator;

  public AuthenticationController(ISender mediator)
  {
    _mediator = mediator;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    var command = new RegisterCommand(
      request.FirstName,
      request.LastName,
      request.Email,
      request.Password);

    ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

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
  public async Task<IActionResult> Login(LoginRequest request)
  {
    var query = new LoginQuery(
      request.Email,
      request.Password);

    // multiple expected custom errors: 
    ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

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