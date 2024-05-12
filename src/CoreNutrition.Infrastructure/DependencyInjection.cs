using Microsoft.Extensions.DependencyInjection;

using CoreNutrition.Infrastructure.Authentication;
using CoreNutrition.Application.Common.Interfaces.Authentication;

namespace CoreNutrition.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddFromInfrastructure(this IServiceCollection services)
  {
    services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    return services;
  }
}