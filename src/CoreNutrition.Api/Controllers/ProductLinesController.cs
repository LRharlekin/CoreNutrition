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
    return Ok(command);
  }
}