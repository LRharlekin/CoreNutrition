using Microsoft.AspNetCore.Mvc;

namespace CoreNutrition.Api.Controllers;

public class ErrorsController : ControllerBase
{
  [Route("/error")]
  public IActionResult Error()
  {
    Problem();
  }
}