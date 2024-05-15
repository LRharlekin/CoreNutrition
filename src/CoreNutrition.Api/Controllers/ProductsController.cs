using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorOr;
using MediatR;
using MapsterMapper;

namespace CoreNutrition.Api.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public class ProductsController : ApiControllerBase
{
  [HttpGet]
  public IActionResult ListProducts()
  {
    return Ok(Array.Empty<string>());
  }
}