using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;


namespace CoreNutrition.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddFromApplication(this IServiceCollection services)
  {
    // services.AddMediatR(typeof(DependencyInjection).Assembly);
    // services.AddMediatR(config => config.AsScoped(), new Assembly[] { typeof(DependencyInjection).Assembly });
    services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
    // services.AddMediatR(typeof(DependencyInjection).Assembly);

    return services;
  }
}




