using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorOr;
using MediatR;
using MapsterMapper;

using CoreNutrition.Api.Infrastructure;
using CoreNutrition.Api.Contracts;
using CoreNutrition.Contracts.ProductLine;
using CoreNutrition.Application.ProductLines.Commands.CreateProductLine;
using CoreNutrition.Domain.ProductLineAggregate;

namespace CoreNutrition.Api.Controllers;

public sealed class ProductLinesController
  : ApiControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public ProductLinesController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpPost(ApiRoutes.ProductLines.Create)]
  public async Task<IActionResult> CreateProductLine(CreateProductLineRequest request)
  {
    await Task.CompletedTask; // TODO delete later
    var command = _mapper.Map<CreateProductLineCommand>(request);
    ErrorOr<ProductLine> createProductLineResult = await _mediator.Send(command);

    return createProductLineResult.Match(
      productLine => CreatedAtAction(
        actionName: nameof(GetProductLineById),
        routeValues: new { productLineId = productLine.Id },
      // value: productLine),
      value: _mapper.Map<ProductLineResponse>(productLine)),
      errors => ResolveProblems(errors)
    );
    // return Ok(createProductLineResult);
  }

  [HttpGet(ApiRoutes.ProductLines.GetById)]
  public async Task<IActionResult> GetProductLineById(Guid productLineId)
  {
    await Task.CompletedTask;
    return Ok(productLineId);
  }
}