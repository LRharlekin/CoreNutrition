using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using CoreNutrition.Infrastructure.Authentication;
using CoreNutrition.Infrastructure.Services;
using CoreNutrition.Application.Common.Interfaces.Authentication;
using CoreNutrition.Application.Common.Interfaces.Services;

namespace CoreNutrition.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddFromInfrastructure(
    this IServiceCollection services,
    ConfigurationManager configuration
  )
  {
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

    return services;
  }
}