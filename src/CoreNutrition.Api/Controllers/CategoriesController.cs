// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authorization;

// using ErrorOr;
// using MediatR;
// using MapsterMapper;

// using CoreNutrition.Api.Infrastructure;
// using CoreNutrition.Api.Contracts;
// using CoreNutrition.Contracts.Category;

// namespace CoreNutrition.Api.Controllers;

// public sealed class CategoriesController : ApiControllerBase
// {
//   [HttpPost]
//   // [Authorize(Roles = "admin")]
//   public IActionResult CreateProduct(CreateCategoryRequest request)
//   {
//     return Ok(Array.Empty<string>());
//   }

//   [AllowAnonymous]
//   [HttpGet]
//   public IActionResult GetCategories()
//   {
//     // return hello world
//     return Ok("Hello World");
//   }
// }