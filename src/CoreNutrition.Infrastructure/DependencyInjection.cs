using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

using CoreNutrition.Infrastructure.Authentication;
using CoreNutrition.Application.Common.Interfaces.Cryptography;
using CoreNutrition.Infrastructure.Cryptography;
using CoreNutrition.Infrastructure.Services;
using CoreNutrition.Domain.Services;
using CoreNutrition.Application.Common.Interfaces.Authentication;
using CoreNutrition.Domain.Common.Interfaces.Services;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Infrastructure.Persistence;
using Microsoft.IdentityModel.Tokens;
// using Microsoft.AspNetCore.DataProtection;

namespace CoreNutrition.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddFromInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {

    services.AddAuth(configuration);
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

    services.AddScoped<IUserRepository, UserRepository>();

    return services;
  }

  public static IServiceCollection AddAuth(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    var jwtSettings = new JwtSettings();
    configuration.Bind(JwtSettings.SectionName, jwtSettings);

    services.AddSingleton(Options.Create(jwtSettings));




    // services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

    // .AddAuthentication() --> returns auth builder, which internally maps between AuthenticationScheme and corresponding authentication handler
    services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
      // .AddJwtBearer() --> specifies the authentication handler to use
      .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        // ValidateAudience = false,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        // ValidIssuer = configuration["JwtSettings:Issuer"],
        ValidAudience = jwtSettings.Audience,
        // ValidAudience = configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
          Encoding.UTF8.GetBytes(jwtSettings.Secret)
        // Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"])
        )
      });

    services.AddTransient<IPasswordHasher, PasswordHasher>();
    services.AddTransient<IPasswordHashChecker, PasswordHasher>();
    return services;
  }
}