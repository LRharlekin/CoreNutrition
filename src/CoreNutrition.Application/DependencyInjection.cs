using Microsoft.Extensions.DependencyInjection;
using CoreNutrition.Application.Services.Authentication.Commands;
using CoreNutrition.Application.Services.Authentication.Queries;

namespace CoreNutrition.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddFromApplication(this IServiceCollection services)
  {
    services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
    services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
    return services;
  }
}