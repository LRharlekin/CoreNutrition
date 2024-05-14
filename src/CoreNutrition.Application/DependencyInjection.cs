using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation;

using CoreNutrition.Application.Common.Behaviors;


namespace CoreNutrition.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddFromApplication(this IServiceCollection services)
  {
    services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
    services.AddScoped(
      typeof(IPipelineBehavior<,>),
      typeof(ValidationBehavior<,>));
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    return services;
  }
}




