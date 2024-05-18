using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorOr;
using MediatR;
using MapsterMapper;

using CoreNutrition.Api.Infrastructure;
using CoreNutrition.Api.Contracts;
using CoreNutrition.Application.Categories.Commands.CreateCategory;
using CoreNutrition.Application.Categories.Commands.UpsertCategory;
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

  [HttpPut(ApiRoutes.Categories.Upsert)]
  public async Task<IActionResult> UpsertCategory(
    Guid categoryId,
    UpsertCategoryRequest request)
  {
    var command = _mapper.Map<UpsertCategoryCommand>((categoryId, request));

    ErrorOr<Category> upsertCategoryResult = await _mediator.Send(command);

    return upsertCategoryResult.Match(
      category => Ok(_mapper.Map<CategoryResponse>(category)),
      errors => ResolveProblems(errors)
    );
  }

  [HttpDelete(ApiRoutes.Categories.Delete)]

  [AllowAnonymous] // TODO Delete later
  [HttpGet(ApiRoutes.Categories.GetById)]
  public async Task<IActionResult> GetCategoryById(Guid categoryId)
  {
    var query = _mapper.Map<GetCategoryByIdQuery>(categoryId);

    ErrorOr<Category> getCategoryByIdResult = await _mediator.Send(query);

    // TODO return 201 if new category was created
    // TODO return 203 if category was updated

    return getCategoryByIdResult.Match(
      category => Ok(_mapper.Map<CategoryResponse>(getCategoryByIdResult)),
      errors => ResolveProblems(errors)
    );
  }

  [AllowAnonymous] // TODO Delete later
  [HttpGet(ApiRoutes.Categories.GetAll)]
  public IActionResult GetAllCategories()
  {
    return Ok("List of categories");
  }

  // [AllowAnonymous]
  // [HttpGet(ApiRoutes.Categories.GetProductLines)]

  // [AllowAnonymous]
  // [HttpGet(ApiRoutes.Categories.GetProducts)]
}