using Microsoft.AspNetCore.Mvc.Infrastructure;

using CoreNutrition.Api.Common.Mapping;
using CoreNutrition.Api.Common.Errors;

namespace CoreNutrition.Api;

public static class DependencyInjection
{
  public static IServiceCollection AddFromPresentation(this IServiceCollection services)
  {
    services.AddControllers();
    services.AddSingleton<ProblemDetailsFactory, CoreNutritionProblemDetailsFactory>();
    services.AddMappings();

    return services;
  }
}




