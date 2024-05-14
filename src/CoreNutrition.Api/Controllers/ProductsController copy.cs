using Microsoft.AspNetCore.Mvc;

using ErrorOr;
using MediatR;
using MapsterMapper;

namespace CoreNutrition.Api.Controllers;

[Route("controller")]
public class ProductsController : ApiControllerBase
{
  [HttpGet]
  public IActionResult ListProducts()
  {
    return Ok(Array.Empty<string>());
  }
}