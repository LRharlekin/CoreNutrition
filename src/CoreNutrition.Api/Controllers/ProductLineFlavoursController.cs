using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorOr;
using MediatR;
using MapsterMapper;

using CoreNutrition.Api.Infrastructure;
using CoreNutrition.Api.Contracts;

using CoreNutrition.Contracts.ProductLineFlavour;
using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Application.ProductLineFlavours.Commands.CreateProductLineFlavour;
// using CoreNutrition.Application.ProductLineFlavours.Queries.ListProductLineFlavours;
// using CoreNutrition.Application.ProductLineFlavours.Queries.GetProductLineFlavourById;

namespace CoreNutrition.Api.Controllers;


public sealed class ProductLineFlavoursController : ApiControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public ProductLineFlavoursController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpPost(ApiRoutes.ProductLineFlavours.Create)]
  public async Task<IActionResult> CreateProductLineFlavour(CreateProductLineFlavourRequest request)
  {
    var command = _mapper.Map<CreateProductLineFlavourCommand>(request);
    ErrorOr<ProductLineFlavour> createProductLineFlavourResult = await _mediator.Send(command);

    return createProductLineFlavourResult.Match(
      productLineFlavour => CreatedAtAction(
        actionName: nameof(GetProductLineFlavourById),
        routeValues: new { productLineFlavourId = productLineFlavour.Id },
        value: _mapper.Map<ProductLineFlavourResponse>(productLineFlavour)),
      errors => ResolveProblems(errors)
    );
  }

  // [HttpPut(ApiRoutes.Categories.Update)]
  // public async Task<IActionResult> UpdateCategory(
  //   Guid categoryId,
  //   UpdateCategoryRequest request)
  // {
  //   var command = _mapper.Map<UpdateCategoryCommand>((categoryId, request));

  //   ErrorOr<Category> updateCategoryResult = await _mediator.Send(command);

  //   // 200 OK
  //   return updateCategoryResult.Match(
  //     category => Ok(_mapper.Map<CategoryResponse>(category)),
  //     errors => ResolveProblems(errors)
  //   );
  // }

  // [HttpDelete(ApiRoutes.Categories.Delete)]
  // {
  //   var command = _mapper.Map<DeleteCategoryCommand>(categoryId);

  //   ErrorOr<??> deleteCategoryResult = await _mediator.Send(command);

  //   return deleteCategoryResult.Match(
  //     ?? => NoContent(), // 204 No Content
  //     errors => ResolveProblems(errors)
  //   );
  // }

  [HttpGet(ApiRoutes.ProductLineFlavours.GetById)]
  public async Task<IActionResult> GetProductLineFlavourById(Guid categoryId)
  {
    await Task.CompletedTask; // TODO delete later
    // var query = _mapper.Map<GetCategoryByIdQuery>(categoryId);

    // ErrorOr<Category> getCategoryByIdResult = await _mediator.Send(query);

    // return getCategoryByIdResult.Match(
    //   category => Ok(_mapper.Map<CategoryResponse>(category)),
    //   errors => ResolveProblems(errors)
    // );
    return Ok();
  }

  // [AllowAnonymous] // TODO Delete later
  // [HttpGet(ApiRoutes.Categories.List)]
  // public async Task<IActionResult> ListCategories()
  // {
  //   var query = new ListCategoriesQuery();

  //   ErrorOr<List<Category>> listCategoriesResult = await _mediator.Send(query);

  //   return listCategoriesResult.Match(
  //     categories => categories.Count > 0
  //       ? Ok(_mapper.Map<List<CategoryResponse>>(categories))
  //       : NoContent(),
  //     errors => ResolveProblems(errors)
  //   );
  // }

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
