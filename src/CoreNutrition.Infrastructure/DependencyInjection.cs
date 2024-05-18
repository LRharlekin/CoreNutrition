using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

using CoreNutrition.Infrastructure.Security.TokenGenerator;
using CoreNutrition.Application.Common.Interfaces.Cryptography;
using CoreNutrition.Infrastructure.Cryptography;
using CoreNutrition.Infrastructure.Services;
using CoreNutrition.Domain.Services;
using CoreNutrition.Application.Common.Interfaces.Authentication;
using CoreNutrition.Domain.Common.Interfaces.Services;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Infrastructure.Users.Persistence;
using CoreNutrition.Infrastructure.Categories.Persistence;
using Microsoft.IdentityModel.Tokens;

namespace CoreNutrition.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddFromInfrastructure(
    this IServiceCollection services,
    ConfigurationManager configuration
  )
  {
    services
      .AddAuth(configuration)
      .AddPersistence();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

    return services;
  }

  public static IServiceCollection AddPersistence(
    this IServiceCollection services
  )
  {
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();

    return services;
  }

  public static IServiceCollection AddAuth(
    this IServiceCollection services,
    ConfigurationManager configuration
  )
  {
    var jwtSettings = new JwtSettings(); // class
    configuration.Bind(JwtSettings.SectionName, jwtSettings);

    services.AddSingleton(Options.Create(jwtSettings));
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

    // .AddAuthentication() --> returns auth builder, which internally maps between AuthenticationScheme and corresponding authentication handler
    services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
      // .AddJwtBearer() --> specifies the authentication handler to use
      .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(jwtSettings.Secret)
        // ValidIssuer = configuration["JwtSettings:Issuer"],
        // ValidAudience = configuration["JwtSettings:Audience"],
        )
      });

    services.AddTransient<IPasswordHasher, PasswordHasher>();
    services.AddTransient<IPasswordHashChecker, PasswordHasher>();
    return services;
  }
}