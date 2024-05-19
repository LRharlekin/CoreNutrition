using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorOr;
using MediatR;
using MapsterMapper;

using CoreNutrition.Api.Infrastructure;
using CoreNutrition.Api.Contracts;
using CoreNutrition.Application.Categories.Commands.CreateCategory;
using CoreNutrition.Application.Categories.Commands.UpdateCategory;
using CoreNutrition.Application.Categories.Queries.GetCategoryById;
using CoreNutrition.Application.Categories.Queries.ListCategories;
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
      category => CreatedAtAction(
        actionName: nameof(GetCategoryById),
        routeValues: new { categoryId = category.Id },
        value: _mapper.Map<CategoryResponse>(category)),
      errors => ResolveProblems(errors)
    );
  }

  [HttpPut(ApiRoutes.Categories.Update)]
  public async Task<IActionResult> UpdateCategory(
    Guid categoryId,
    UpdateCategoryRequest request)
  {
    var command = _mapper.Map<UpdateCategoryCommand>((categoryId, request));

    ErrorOr<Category> updateCategoryResult = await _mediator.Send(command);

    // 200 OK
    return updateCategoryResult.Match(
      category => Ok(_mapper.Map<CategoryResponse>(category)),
      errors => ResolveProblems(errors)
    );
  }

  // [HttpDelete(ApiRoutes.Categories.Delete)]
  // {
  //   var command = _mapper.Map<DeleteCategoryCommand>(categoryId);

  //   ErrorOr<??> deleteCategoryResult = await _mediator.Send(command);

  //   return deleteCategoryResult.Match(
  //     ?? => NoContent(), // 204 No Content
  //     errors => ResolveProblems(errors)
  //   );
  // }

  [HttpGet(ApiRoutes.Categories.GetById)]
  public async Task<IActionResult> GetCategoryById(Guid categoryId)
  {
    var query = _mapper.Map<GetCategoryByIdQuery>(categoryId);

    ErrorOr<Category> getCategoryByIdResult = await _mediator.Send(query);

    return getCategoryByIdResult.Match(
      category => Ok(_mapper.Map<CategoryResponse>(category)),
      errors => ResolveProblems(errors)
    );
  }

  [AllowAnonymous] // TODO Delete later
  [HttpGet(ApiRoutes.Categories.List)]
  public async Task<IActionResult> ListCategories()
  {
    var query = new ListCategoriesQuery();

    ErrorOr<List<Category>> listCategoriesResult = await _mediator.Send(query);

    return listCategoriesResult.Match(
      categories => categories.Count > 0
        ? Ok(_mapper.Map<List<CategoryResponse>>(categories))
        : NoContent(),
      errors => ResolveProblems(errors)
    );
  }

  // [AllowAnonymous]
  // [HttpGet(ApiRoutes.Categories.GetProductLines)]

  // [AllowAnonymous]
  // [HttpGet(ApiRoutes.Categories.GetProducts)]

  // private CreatedAtActionResult CreatedAtGetCategory(Category category)
  // {
  //   return CreatedAtAction(
  //     actionName: nameof(GetCategoryById),
  //     routeValues: new { categoryId = category.Id },
  //     value: _mapper.Map<CategoryResponse>(category)
  //   );
  // }
}