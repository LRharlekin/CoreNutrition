using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Filters;

using CoreNutrition.Api.Common.Errors;
using CoreNutrition.Api.Common.Mapping;
using CoreNutrition.Application;
using CoreNutrition.Infrastructure;
using CoreNutrition.Api;

var builder = WebApplication.CreateBuilder(args);

{
  builder.Services
    .AddFromPresentation()
    .AddFromApplication()
    .AddFromInfrastructure(builder.Configuration);

  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen(options =>
  // configs to enable Authorization: header when using SwaggerUI
    {
      options.EnableAnnotations();

      options.SwaggerDoc("v1", new OpenApiInfo
      {
        Version = "v1",
        Title = "Core Nutrition API",
        Description = "API for interacting with and managing the online shop for the (completely fictitious) Core Nutrition brand of health supplements.",
        Contact = new OpenApiContact
        {
          Name = "Lukas Rappen",
          Url = new Uri("https://www.github.com/LRharlekin")
        }
      });

      options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
      });

      options.OperationFilter<SecurityRequirementsOperationFilter>();
    }
  );

}

var app = builder.Build();

{
  // Configure the HTTP request pipeline.
  app.UseSwagger();
  // if (app.Environment.IsDevelopment())
  // {
  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
  });
  // }

  app.UsePathBase("/api/v1");
  app.UseHttpsRedirection();

  app.UseAuthentication();
  app.UseAuthorization();

  app.MapControllers();
  app.MapGet("/", () => "Hello World!");
  await app.RunAsync();
}