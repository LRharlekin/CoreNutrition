using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

{
  builder.Services.AddEndpointsApiExplorer();

  builder.Services.AddSwaggerGen();

  builder.Services.AddControllers();

  // builder.Services.AddAuthentication();
  // builder.Services.AddAuthorization();
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
  app.MapControllers();
  app.MapGet("/", () => "Hello World!");
}

await app.RunAsync();