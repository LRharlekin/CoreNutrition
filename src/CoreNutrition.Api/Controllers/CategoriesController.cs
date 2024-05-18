using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorOr;
using MediatR;
using MapsterMapper;

using CoreNutrition.Api.Infrastructure;
using CoreNutrition.Api.Contracts;
using CoreNutrition.Application.Categories.Commands.CreateCategory;
using CoreNutrition.Application.Categories.Common;
using CoreNutrition.Contracts.Category;

namespace CoreNutrition.Api.Controllers;

public sealed class CategoriesController : ApiControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public CategoriesController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpPost(ApiRoutes.Categories.Create)]
  public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
  {
    var command = _mapper.Map<CreateCategoryCommand>(request);
    ErrorOr<CategoryResult> createCategoryResult = await _mediator.Send(command);


    /*

    if (actionResult.IsError && actionResult.FirstError == Errors.Authentication.InvalidCredentials)
    {
      return Problem(
        statusCode: StatusCodes.StatusXXXType,
        title: actionResult.FirstError.Description);
    }

    return actionResult.Match(
      actionResult => Ok(_mapper.Map<ResponseType>(actionResult)),
      errors => ResolveProblems(errors)
      );
  }
  } 
  */
    // return Ok(request);
    return createCategoryResult.Match(
      category => Ok(_mapper.Map<CategoryResponse>(category)),
      errors => ResolveProblems(errors)
    );
  }

  [AllowAnonymous]
  [HttpGet(ApiRoutes.Categories.GetAll)]
  public IActionResult GetAllCategories()
  {
    // return hello world
    return Ok("List of categories");
  }
}