using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CoreNutrition.Api.Controllers;

public class ErrorsController : ControllerBase
{
  [Route("/error")]
  [HttpGet] // TODO: maybe delete later - only added for Swagger
  public IActionResult Error()
  {
    Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    return Problem();
  }
}