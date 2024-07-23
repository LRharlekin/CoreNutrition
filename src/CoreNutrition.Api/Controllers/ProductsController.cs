using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorOr;
using MediatR;
using MapsterMapper;

using CoreNutrition.Api.Infrastructure;
using CoreNutrition.Api.Contracts;
using CoreNutrition.Contracts.Product;
using CoreNutrition.Application.Products.Commands.CreateProduct;
using CoreNutrition.Domain.ProductAggregate;

namespace CoreNutrition.Api.Controllers;

public sealed class ProductsController
  : ApiControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public ProductsController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpPost(ApiRoutes.Products.Create)]
  public async Task<IActionResult> CreateProduct(CreateProductRequest request)
  {
    await Task.CompletedTask; // TODO delete later
    var command = _mapper.Map<CreateProductCommand>(request);
    // ErrorOr<Product> createProductResult = await _mediator.Send(command);

    // return createProductResult.Match(
    //   product => CreatedAtAction(
    //     actionName: nameof(GetProductById),
    //     routeValues: new { productId = product.Id },
    //     value: _mapper.Map<ProductResponse>(product)),
    //   errors => ResolveProblems(errors)
    // );
    return Ok(command);
  }

  // [HttpGet]
  // public IActionResult ListProducts()
  // {
  //   return Ok(Array.Empty<string>());
  // }
}