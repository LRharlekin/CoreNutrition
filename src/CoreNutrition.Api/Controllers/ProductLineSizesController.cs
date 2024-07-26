using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorOr;
using MediatR;
using MapsterMapper;

using CoreNutrition.Api.Infrastructure;
using CoreNutrition.Api.Contracts;

using CoreNutrition.Contracts.ProductLineSize;
using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Application.ProductLineSizes.Commands.CreateProductLineSize;
// using CoreNutrition.Application.ProductLineSizes.Queries.ListProductLineSizes;
// using CoreNutrition.Application.ProductLineSizes.Queries.GetProductLineSizeById;

namespace CoreNutrition.Api.Controllers;


public sealed class ProductLineSizesController : ApiControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public ProductLineSizesController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpPost(ApiRoutes.ProductLineSizes.Create)]
  public async Task<IActionResult> CreateProductLineSize(CreateProductLineSizeRequest request)
  {
    await Task.CompletedTask; // TODO delete later
    var command = _mapper.Map<CreateProductLineSizeCommand>(request);
    ErrorOr<ProductLineSize> createProductLineSizeResult = await _mediator.Send(command);

    return createProductLineSizeResult.Match(
      productLineSize => CreatedAtAction(
        actionName: nameof(GetProductLineSizeById),
        routeValues: new { productLineSizeId = productLineSize.Id },
        value: _mapper.Map<ProductLineSizeResponse>(productLineSize)),
      errors => ResolveProblems(errors)
    );
  }

  // [HttpPut(ApiRoutes.ProductLineSizes.Update)]
  // public async Task<IActionResult> UpdateProductLineSize(
  //   Guid productLineSizeId,
  //   UpdateProductLineSizeRequest request)
  // {
  //   var command = _mapper.Map<UpdateProductLineSizeCommand>((productLineSizeId, request));

  //   ErrorOr<Category> updateProductLineSizeResult = await _mediator.Send(command);

  //   // 200 OK
  //   return updateProductLineSizeResult.Match(
  //     category => Ok(_mapper.Map<CategoryResponse>(category)),
  //     errors => ResolveProblems(errors)
  //   );
  // }

  // [HttpDelete(ApiRoutes.ProductLineSizes.Delete)]
  // {
  //   var command = _mapper.Map<DeleteCategoryCommand>(productLineSizeId);

  //   ErrorOr<??> deleteCategoryResult = await _mediator.Send(command);

  //   return deleteCategoryResult.Match(
  //     ?? => NoContent(), // 204 No Content
  //     errors => ResolveProblems(errors)
  //   );
  // }

  [HttpGet(ApiRoutes.ProductLineSizes.GetById)]
  public async Task<IActionResult> GetProductLineSizeById(Guid productLineSizeId)
  {
    await Task.CompletedTask; // TODO delete later
    //   var query = _mapper.Map<GetProductLineSizeByIdQuery>(productLineSizeId);

    //   ErrorOr<ProductLineSize> getProductLineSizeByIdResult = await _mediator.Send(query);

    //   return getProductLineSizeByIdResult.Match(
    //     productLineSize => Ok(_mapper.Map<ProductLineSizeResponse>(productLineSize)),
    //     errors => ResolveProblems(errors)
    //   );
    // }

    // [HttpGet(ApiRoutes.ProductLineSizes.List)]
    // public async Task<IActionResult> ListProductLineSizes()
    // {
    //   var query = new ListProductLineSizesQuery(); // TODO: add sorting

    //   ErrorOr<List<ProductLineSize>> listProductLineSizesResult = await _mediator.Send(query);

    //   return listProductLineSizesResult.Match(
    //     productLineSizes => productLineSizes.Count > 0
    //       ? Ok(_mapper.Map<List<ProductLineSizeResponse>>(productLineSizes))
    //       // ? Ok(_mapper.Map<ListProductLineSizesResponse>(productLineSizes))
    //       : NoContent(),
    //     errors => ResolveProblems(errors)
    //   );
    return Ok(productLineSizeId);
  }

  // [HttpGet(ApiRoutes.ProductLineSizes.GetByProductLine)]
  // public async Task<IActionResult> GetProductLineSizesByProductLine(Guid productLineId)
}
