using Microsoft.AspNetCore.Mvc;

using ErrorOr;

using CoreNutrition.Api.Common.Http;

namespace CoreNutrition.Api.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
  protected IActionResult Problem(List<Error> errors)
  {
    /* 
    HttpContext.Items gets/sets a key-value collection that can be used to share data between middleware within the scope of this request.
    */
    HttpContext.Items[HttpContextItemKeys.Errors] = errors;

    var firstError = errors[0];

    var statusCode = firstError.Type switch
    {
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      _ => StatusCodes.Status500InternalServerError,
    };

    return Problem(statusCode: statusCode, title: firstError.Description);
  }
}