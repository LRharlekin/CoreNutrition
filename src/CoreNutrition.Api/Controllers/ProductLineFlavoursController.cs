using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorOr;
using MediatR;
using MapsterMapper;
using Swashbuckle.AspNetCore.Annotations;

using CoreNutrition.Api.Infrastructure;
using CoreNutrition.Api.Contracts;

using CoreNutrition.Contracts.ProductLineFlavour;
using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Application.ProductLineFlavours.Commands.CreateProductLineFlavour;
using CoreNutrition.Application.ProductLineFlavours.Queries.ListProductLineFlavours;
using CoreNutrition.Application.ProductLineFlavours.Queries.GetProductLineFlavourById;

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
  [SwaggerOperation(Summary = "Create a new product line flavour", Description = "Creates a new product line flavour with the provided details.")]
  [SwaggerResponse(StatusCodes.Status201Created, "Product line flavour created successfully", typeof(ProductLineFlavourResponse))]
  [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
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

  // [HttpPut(ApiRoutes.ProductLineFlavours.Update)]
  // public async Task<IActionResult> UpdateProductLineFlavour(
  //   Guid productLineFlavourId,
  //   UpdateProductLineFlavourRequest request)
  // {
  //   var command = _mapper.Map<UpdateProductLineFlavourCommand>((productLineFlavourId, request));

  //   ErrorOr<Category> updateProductLineFlavourResult = await _mediator.Send(command);

  //   // 200 OK
  //   return updateProductLineFlavourResult.Match(
  //     category => Ok(_mapper.Map<CategoryResponse>(category)),
  //     errors => ResolveProblems(errors)
  //   );
  // }

  // [HttpDelete(ApiRoutes.ProductLineFlavours.Delete)]
  // {
  //   var command = _mapper.Map<DeleteCategoryCommand>(productLineFlavourId);

  //   ErrorOr<??> deleteCategoryResult = await _mediator.Send(command);

  //   return deleteCategoryResult.Match(
  //     ?? => NoContent(), // 204 No Content
  //     errors => ResolveProblems(errors)
  //   );
  // }

  [HttpGet(ApiRoutes.ProductLineFlavours.GetById)]
  [SwaggerOperation(Summary = "Get a product line flavour by ID", Description = "Retrieves a specific product line flavour by its unique identifier.")]
  [SwaggerResponse(StatusCodes.Status200OK, "Product line flavour retrieved successfully", typeof(ProductLineFlavourResponse))]
  [SwaggerResponse(StatusCodes.Status404NotFound, "Product line flavour not found")]
  public async Task<IActionResult> GetProductLineFlavourById(Guid productLineFlavourId)
  {
    await Task.CompletedTask; // TODO delete later
    var query = _mapper.Map<GetProductLineFlavourByIdQuery>(productLineFlavourId);

    ErrorOr<ProductLineFlavour> getProductLineFlavourByIdResult = await _mediator.Send(query);

    return getProductLineFlavourByIdResult.Match(
      productLineFlavour => Ok(_mapper.Map<ProductLineFlavourResponse>(productLineFlavour)),
      errors => ResolveProblems(errors)
    );
  }

  [HttpGet(ApiRoutes.ProductLineFlavours.List)]
  [SwaggerOperation(Summary = "List all product line flavours", Description = "Retrieves a list of all product line flavours.")]
  [SwaggerResponse(StatusCodes.Status200OK, "Product line flavours retrieved successfully", typeof(List<ProductLineFlavourResponse>))]
  [SwaggerResponse(StatusCodes.Status204NoContent, "No product line flavours found")]
  public async Task<IActionResult> ListProductLineFlavours()
  {
    var query = new ListProductLineFlavoursQuery(); // TODO: add sorting

    ErrorOr<List<ProductLineFlavour>> listProductLineFlavoursResult = await _mediator.Send(query);

    return listProductLineFlavoursResult.Match(
      productLineFlavours => productLineFlavours.Count > 0
        ? Ok(_mapper.Map<List<ProductLineFlavourResponse>>(productLineFlavours))
        // ? Ok(_mapper.Map<ListProductLineFlavoursResponse>(productLineFlavours))
        : NoContent(),
      errors => ResolveProblems(errors)
    );
  }

  // [HttpGet(ApiRoutes.ProductLineFlavours.GetByProductLine)]
  // public async Task<IActionResult> GetProductLineFlavoursByProductLine(Guid productLineId)
}
