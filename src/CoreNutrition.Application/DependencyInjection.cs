using Microsoft.Extensions.DependencyInjection;
using CoreNutrition.Application.Services.Authentication;

namespace CoreNutrition.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddFromApplication(this IServiceCollection services)
  {
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    return services;
  }
}