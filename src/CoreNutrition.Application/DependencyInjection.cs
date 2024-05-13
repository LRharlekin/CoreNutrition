using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;


namespace CoreNutrition.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddFromApplication(this IServiceCollection services)
  {
    services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

    return services;
  }
}




