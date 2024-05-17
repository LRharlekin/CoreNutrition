using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorOr;
using MediatR;
using MapsterMapper;

using CoreNutrition.Api.Infrastructure;
using CoreNutrition.Api.Contracts;
using CoreNutrition.Contracts.Category;

namespace CoreNutrition.Api.Controllers;

public sealed class CategoriesController : ApiControllerBase
{
  [HttpPost(ApiRoutes.Categories.Create)]
  [Authorize(Roles = "Admin")]
  public IActionResult CreateCategory(CreateCategoryRequest request)
  {
    return Ok("Create a category");
  }

  [AllowAnonymous]
  [HttpGet(ApiRoutes.Categories.GetAll)]
  public IActionResult GetAllCategories()
  {
    // return hello world
    return Ok("List of categories");
  }
}