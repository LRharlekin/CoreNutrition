using Microsoft.AspNetCore.Mvc;

using ErrorOr;

using CoreNutrition.Api.Common.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CoreNutrition.Api.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
  protected IActionResult ResolveProblems(List<Error> errors)
  {
    if (errors.Count == 0)
    {
      return Problem(); // from ControllerBase
    }

    if (errors.All(error => error.Type == ErrorType.Validation))
    {
      return ResolveValidationProblems(errors);
    }

    /* 
    if (errors.All(error => error.Type == ErrorType.Conflict))
    {
      return ResolveConflictProblems(errors);
    }

    if (errors.All(error => error.Type == ErrorType.NotFound))
    {
      return ResolveNotFoundProblems(errors);
    }

    if (errors.All(error => error.Type == ErrorType.Unauthorized))
    {
      return ResolveUnauthorizedProblems(errors);
    }

    if (errors.All(error => error.Type == ErrorType.Forbidden))
    {
      return ResolveForbiddenProblems(errors);
    }
    */

    /* 
    if (errors.All(error => error.NumericType == 123)) // custom logic for Domain Errors?
    {
      return ResolveBadRequestProblems(errors);
    }
    */

    /* 
    HttpContext.Items gets/sets a key-value collection that can be used to share data between middleware within the scope of this request.
    */
    HttpContext.Items[HttpContextItemKeys.Errors] = errors;

    var firstError = errors[0];

    /* 
    if (firstError.NumericType == 123)
    {
      // custom logic for Domain Errors?
    }
    */
    var statusCode = GetStatusCodeFromErrorType(firstError.Type);

    return Problem(statusCode: statusCode, title: firstError.Description); // from ControllerBase
  }

  protected IActionResult ResolveProblems(Error error)
  {
    var statusCode = GetStatusCodeFromErrorType(error.Type);

    return Problem(
      statusCode: statusCode,
      title: error.Description); // from ControllerBase
  }

  private int GetStatusCodeFromErrorType(ErrorType errorType)
  {
    return errorType switch
    {
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
      ErrorType.Forbidden => StatusCodes.Status403Forbidden,
      _ => StatusCodes.Status500InternalServerError,
    };
  }
  private IActionResult ResolveValidationProblems(List<Error> errors)
  {
    var modelStateDictionary = new ModelStateDictionary();

    foreach (var error in errors)
    {
      modelStateDictionary.AddModelError(
        error.Code,
        error.Description);
    }

    return ValidationProblem(modelStateDictionary); // from ControllerBase
  }
}