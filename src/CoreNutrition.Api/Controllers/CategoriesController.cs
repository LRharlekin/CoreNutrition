using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorOr;
using MediatR;
using MapsterMapper;

using CoreNutrition.Api.Infrastructure;
using CoreNutrition.Api.Contracts;
using CoreNutrition.Application.Categories.Commands.CreateCategory;
using CoreNutrition.Application.Categories.Queries.GetCategoryById;
using CoreNutrition.Contracts.Category;
using CoreNutrition.Domain.CategoryAggregate;

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
    ErrorOr<Category> createCategoryResult = await _mediator.Send(command);

    return createCategoryResult.Match(
      // cateogry => CreatedAtAction()) // TODO: 201 Created
      category => Ok(_mapper.Map<CategoryResponse>(category)),
      errors => ResolveProblems(errors)
    );
  }

  // [HttpPatch(ApiRoutes.Categories.Update)]
  // public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest request)
  // {
  //   var command = _mapper.Map<UpdateCategoryCommand>(request);

  //   ErrorOr<Category> updateCategoryResult = await _mediator.Send(command);

  //   return updateCategoryResult.Match(
  //     category => Ok(_mapper.Map<CategoryResponse>(category)),
  //     errors => ResolveProblems(errors)
  //   );
  // }

  [AllowAnonymous]
  [HttpGet(ApiRoutes.Categories.GetById)]
  public async Task<IActionResult> GetCategoryById(Guid categoryId)
  {
    var query = _mapper.Map<GetCategoryByIdQuery>(categoryId);

    ErrorOr<Category> getCategoryByIdResult = await _mediator.Send(query);

    return getCategoryByIdResult.Match(
      category => Ok(_mapper.Map<CategoryResponse>(getCategoryByIdResult)),
      errors => ResolveProblems(errors)
    );
  }

  [AllowAnonymous]
  [HttpGet(ApiRoutes.Categories.GetAll)]
  public IActionResult GetAllCategories()
  {
    return Ok("List of categories");
  }
}