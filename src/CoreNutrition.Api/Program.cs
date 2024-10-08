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
  // configs for enable Authorization: header when using SwaggerUI
    {
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
  // if (app.Environment.IsDevelopment())
  // {
  app.UseSwagger();
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