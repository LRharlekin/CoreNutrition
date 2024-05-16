using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorOr;
using MediatR;
using MapsterMapper;

using CoreNutrition.Contracts.Category;

namespace CoreNutrition.Api.Controllers;

[Route("[controller]")]
public class CategoriesController : ApiControllerBase
{
  [HttpPost]
  // [Authorize(Roles = "admin")]
  public IActionResult CreateProduct(CreateCategoryRequest request)
  {
    return Ok(Array.Empty<string>());
  }

  [AllowAnonymous]
  [HttpGet]
  public IActionResult GetCategories()
  {
    // return hello world
    return Ok("Hello World");
  }
}