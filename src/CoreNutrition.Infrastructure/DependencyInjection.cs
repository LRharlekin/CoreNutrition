using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using CoreNutrition.Infrastructure.Authentication;
using CoreNutrition.Infrastructure.Services;
using CoreNutrition.Application.Common.Interfaces.Authentication;
using CoreNutrition.Domain.Common.Interfaces.Services;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Infrastructure.Persistence;

namespace CoreNutrition.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddFromInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

    services.AddScoped<IUserRepository, UserRepository>();

    return services;
  }
}