using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

using ErrorOr;
using MediatR;
using MapsterMapper;

using CoreNutrition.Api.Infrastructure;
using CoreNutrition.Api.Contracts;
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

public sealed class AuthenticationController : ApiControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public AuthenticationController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [AllowAnonymous]
  [HttpPost(ApiRoutes.Authentication.Register)]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    var command = _mapper.Map<RegisterCommand>(request);

    ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

    return authResult.Match(
      authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
      errors => ResolveProblems(errors)
      );
  }

  [AllowAnonymous]
  [HttpPost(ApiRoutes.Authentication.Login)]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    var query = _mapper.Map<LoginQuery>(request);

    ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

    if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
    {
      return Problem(
        statusCode: StatusCodes.Status401Unauthorized,
        title: authResult.FirstError.Description);
    }

    return authResult.Match(
      authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
      errors => ResolveProblems(errors)
      );
  }
}